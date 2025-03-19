using System.Text.Json;
using Azure.Storage.Blobs.Models;
using MatchHype.Api.DataProvider;
using Microsoft.AspNetCore.WebUtilities;

namespace MatchHypePlatform.Features.DataProvider
{
    public static class Helpers
    {
        public const string ApiKey = "xx";
        public const string ApiKey2 = "?api_token=xx";
        public const string ApiKeyFS = "xxx";
        
        public static async Task<T> GetFootyStatsDataAsync<T>(string apiEndpoint, Dictionary<string, string> parameters)
        {
            // Base URL for the API
            string baseUrl = "https://api.football-data-api.com/";

            // Manually append the API key directly to the URL
            string url = $"{baseUrl}{apiEndpoint}?key={ApiKeyFS}";

            // Add remaining query string parameters (excluding the API key)
            url = QueryHelpers.AddQueryString(url, parameters);

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonSerializer.Deserialize<T>(responseBody);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    Console.WriteLine($"Response Body: {responseBody}");
                    throw new Exception($"Request failed with status code: {response.StatusCode}");
                }
            }
        }

        public static string GetLeagueName(int leagueId)
        {
            Dictionary<int, string> leagues = new Dictionary<int, string>
        {
            {12336, "Chance Liga"},
            {12316, "La Liga"},
            {12529, "Bundesliga"},
            {12530, "Serie A"},
            {12337, "Ligue 1"},
            {12325, "Premier League"},
            {12321, "Champions League"}
        };

            if (leagues.TryGetValue(leagueId, out string leagueName))
            {
                return leagueName;
            }
            else
            {
                return $"Unknown league ID: {leagueId}";
            }
        }

        public static Tuple<string, string> GetTeamNameEuro(string englishName)
        {
            Dictionary<string, Tuple<string, string>> teamsEuro = new Dictionary<string, Tuple<string, string>>
                {
                    {"Albania", Tuple.Create("Albánie", "ALB")},   //CZname, CodeName  
                    {"Austria", Tuple.Create("Rakousko", "AUT")},
                    {"Belgium", Tuple.Create("Belgie", "BEL")},
                    {"Croatia", Tuple.Create("Chorvatsko", "CRO")},
                    {"Czech Republic", Tuple.Create("Česká republika", "CZE")},
                    {"Denmark", Tuple.Create("Dánsko", "DEN")},
                    {"England", Tuple.Create("Anglie", "ENG")},
                    {"France", Tuple.Create("Francie", "FRA")},
                    {"Georgia", Tuple.Create("Gruzie", "GEO")},
                    {"Germany", Tuple.Create("Německo", "GER")},
                    {"Hungary", Tuple.Create("Maďarsko", "HUN")},
                    {"Italy", Tuple.Create("Itálie", "ITA")},
                    {"Netherlands", Tuple.Create("Nizozemsko", "NED")},
                    {"Poland", Tuple.Create("Polsko", "POL")},
                    {"Portugal", Tuple.Create("Portugalsko", "POR")},
                    {"Romania", Tuple.Create("Rumunsko", "ROU")},
                    {"Scotland", Tuple.Create("Skotsko", "SCO")},
                    {"Serbia", Tuple.Create("Srbsko", "SRB")},
                    {"Slovakia", Tuple.Create("Slovensko", "SVK")},
                    {"Slovenia", Tuple.Create("Slovinsko", "SVN")},
                    {"Spain", Tuple.Create("Španělsko", "ESP")},
                    {"Switzerland", Tuple.Create("Švýcarsko", "SUI")},
                    {"Turkey", Tuple.Create("Turecko", "TUR")},
                    {"Ukraine", Tuple.Create("Ukrajina", "UKR")}
                };

            if (teamsEuro.TryGetValue(englishName, out Tuple<string, string> teamInfo))
            {
                return teamInfo;
            }
            else
            {
                return Tuple.Create($"Unknown team name: {englishName}", "");
            }
        }

        public static string GetStadiumCapacity(string stadiumName)
        {
            Dictionary<string, string> stadiumCapacity = new Dictionary<string, string>
                {
                    {"Olympiastadion Berlin", "74461"},
                    {"RheinEnergieStadion", "49827"},
                    {"SIGNAL IDUNA PARK", "65849"},
                    {"Merkur Spiel-Arena", "51031"},
                    {"Deutsche Bank Park", "48387"},
                    {"VELTINS-Arena", "54740"},
                    {"Volksparkstadion", "52245"},
                    {"Red Bull Arena", "49539"},
                    {"Allianz Arena", "70076"},
                    {"MHP Arena", "54697"}
                };
            if (stadiumCapacity.TryGetValue(stadiumName, out string capacity))
            {
                return capacity;
            }
            else
            {
                return $"Unknown stadium name: {stadiumName}";
            }
        }

        public static string GetStadiumCityName(string stadiumName)
        {
            Dictionary<string, string> stadiumCities = new Dictionary<string, string>
                {
                    {"Olympiastadion Berlin", "Berlín"},
                    {"RheinEnergieStadion", "Kolín nad Rýnem"},
                    {"SIGNAL IDUNA PARK", "Dortmund"},
                    {"Merkur Spiel-Arena", "Düsseldorf"},
                    {"Deutsche Bank Park", "Frankfurt"},
                    {"VELTINS-Arena", "Gelsenkirchen"},
                    {"Volksparkstadion", "Hamburk"},
                    {"Red Bull Arena", "Lipsko"},
                    {"Allianz Arena", "Mnichov"},
                    {"MHP Arena", "Stuttgart"}
                };

            if (stadiumCities.TryGetValue(stadiumName, out string cityName))
            {
                return cityName;
            }
            else
            {
                return $"Unknown stadium name: {stadiumName}";
            }
        }
    }
}
