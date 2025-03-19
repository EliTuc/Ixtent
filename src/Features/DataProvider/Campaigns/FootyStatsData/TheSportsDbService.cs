
using CsvHelper;
using CsvHelper.Configuration;
using MatchHype.Api.DataProvider;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Json;
using MatchHypePlatform.Features.DataProvider;
using System.Text.RegularExpressions;

namespace MatchHype.Api.Features.DataProvider.Campaigns.FootyStatsData
{
    public class TheSportsDbService
    {
        private const string CZ = "1";
        private const string EN = "2";
        private const string DE = "3";

        // Generate initial CSV file for round data
        public async Task<IActionResult> GenerateRoundInitialCsv(string matchDate, string language)
        {
            // Get data from FootyStats API for today's matches and select by LeagueID
            var leagueDataMatches = await Helpers.GetFootyStatsDataAsync<TodaysMatchRoot>("todays-matches", new Dictionary<string, string>
            {
                { "date", $"{matchDate}" }
            });

            var matchIDs = new List<int>();
            var championshipIds = new HashSet<int> { 12316, 12529, 12530, 12325, 12336, 12337 };

            if (leagueDataMatches?.Data != null)
            {
                foreach (var leagueMatch in leagueDataMatches.Data)
                {
                    if (championshipIds.Contains(leagueMatch.ChampionshipId))
                    {
                        matchIDs.Add(leagueMatch.MatchId);
                    }
                }
            }

            var eventRecords = new List<InputParameters>();

            foreach (var matchID in matchIDs)
            {
                // Get match details by match ID
                var match = await Helpers.GetFootyStatsDataAsync<BettingOddsRoot>("match", new Dictionary<string, string>
                {
                    { "match_id", $"{matchID}" }
                });

                var actualEvent = match.Data;
                var eventRecord = new InputParameters
                {
                    ChampionshipID = actualEvent.ChampionshipID,
                    League_Name = Helpers.GetLeagueName(actualEvent.ChampionshipID),
                    Language = language
                };

                // Extract stadiumName and cityName from footystats Stadium value using regex
                Regex regex = new Regex(@"^([^\(]+)\s*\(([^)]+)\)");
                var regexMatch = regex.Match(actualEvent.StadiumName);
                if (regexMatch.Success)
                {
                    eventRecord.StadiumName = regexMatch.Groups[1].Value;
                    eventRecord.CityName = regexMatch.Groups[2].Value;
                }

                // Convert Unix timestamp to readable date and time in Prague timezone
                var dateUnix = actualEvent.DateUnix;
                DateTime utcDateTime = DateTimeOffset.FromUnixTimeSeconds(dateUnix).UtcDateTime;
                TimeZoneInfo pragueTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time");
                DateTime pragueDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, pragueTimeZone);
                eventRecord.MatchDate = pragueDateTime.ToString("dd.MM.yyyy");
                eventRecord.Time = pragueDateTime.ToString("HH:mm");

                // Get the team IDs
                int homeTeamId = actualEvent.HT_ID;
                int awayTeamId = actualEvent.AT_ID;

                // Get the odds for predictions
                var odds_ft_1 = actualEvent.HT_Prediction;
                var odds_ft_x = actualEvent.Draw_Prediction;
                var odds_ft_2 = actualEvent.AT_Prediction;

                // Calculate implied probabilities
                var hT_Prediction = 1 / odds_ft_1;
                var draw_Prediction = 1 / odds_ft_x;
                var at_Prediction = 1 / odds_ft_2;
                var totalPredictions = hT_Prediction + draw_Prediction + at_Prediction;

                // Normalize the implied probabilities to 100%
                eventRecord.HT_Prediction = Math.Round((hT_Prediction / totalPredictions) * 100, 0).ToString();
                eventRecord.Draw_Prediction = Math.Round((draw_Prediction / totalPredictions) * 100, 0).ToString();
                eventRecord.AT_Prediction = Math.Round((at_Prediction / totalPredictions) * 100, 0).ToString();

                // Get H2H (head-to-head) data
                var h2hData = actualEvent.H2H;
                var lastFiveMatches = h2hData.PreviousMatchesIds.Take(5).ToList();

                int homeWins = 0;
                int awayWins = 0;
                int draws = 0;

                for (int i = 0; i < lastFiveMatches.Count; i++)
                {
                    var previousMatch = lastFiveMatches[i];

                    int homeGoals = previousMatch.Team_A_ID == homeTeamId ? previousMatch.Team_A_Goals : previousMatch.Team_B_Goals;
                    int awayGoals = previousMatch.Team_B_ID == awayTeamId ? previousMatch.Team_B_Goals : previousMatch.Team_A_Goals;

                    if (homeGoals > awayGoals) homeWins++;
                    else if (awayGoals > homeGoals) awayWins++;
                    else draws++;

                    if (i == 0) // Save last match result
                    {
                        eventRecord.HT_Last_Match_Score = homeGoals;
                        eventRecord.AT_Last_Match_Score = awayGoals;
                        eventRecord.Last_Match_Date = DateTimeOffset.FromUnixTimeSeconds(previousMatch.DateUnixLast).DateTime.ToString("dd.MM.yyyy");
                    }
                }

                eventRecord.HT_H2H_W = homeWins;
                eventRecord.AT_H2H_W = awayWins;
                eventRecord.HT_AT_Draw = draws;

                // Get team data from ElevenLabsInput
                string jsonPath = @"C:\Projects\DataManager\src\ElevenLabsInput.txt";
                string jsonString = File.ReadAllText(jsonPath);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var teams = JsonSerializer.Deserialize<List<JsonTeamData>>(jsonString, options);

                // Find home and away team data
                var homeTeam = teams.FirstOrDefault(t => t.Id == homeTeamId);
                var awayTeam = teams.FirstOrDefault(t => t.Id == awayTeamId);

                if (homeTeam != null) eventRecord.HT_Name = homeTeam.TeamName;
                if (awayTeam != null) eventRecord.AT_Name = awayTeam.TeamName;

                // Add to records
                eventRecords.Add(eventRecord);
            }

            // Export to CSV
            await ExportEventsToCsv(eventRecords, matchDate);

            return new OkObjectResult("Successfully generated CSV file.");
        }

        // Export events to CSV using CsvHelper
        public async Task ExportEventsToCsv(List<InputParameters> eventRecords, string round)
        {
            string filePath = $"{Directory.GetCurrentDirectory()}/Outputs-Uploads/round{round}.csv";
            using var writer = new StreamWriter(filePath);
            using var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" });
            await csv.WriteRecordsAsync(eventRecords);
        }
    }
}
