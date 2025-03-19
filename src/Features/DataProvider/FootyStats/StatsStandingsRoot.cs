using System.Text.Json.Serialization;

namespace MatchHype.Api.DataProvider
{
    public class StatsStandingsRoot
    {
        [JsonPropertyName("data")]
        public StatsTable Data { get; set; }
    }

    public class StatsTable
    {
        [JsonPropertyName("league_table")]
        public object LeagueTable { get; set; }

        [JsonPropertyName("specific_tables")]
        public List<SpecTable> SpecificTables { get; set; }

        [JsonPropertyName("all_matches_table_overall")]
        public List<AllMatchesTableOverall> AllMatchesTableOverall { get; set; }
    }

    public class SpecTable
    {
        [JsonPropertyName("round")]
        public string Round { get; set; }

        [JsonPropertyName("table")]
        public List<Standings> Table { get; set; }
    }

    public class Standings
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("cleanName")]
        public string CleanName { get; set; }

        [JsonPropertyName("points")]
        public int Points { get; set; }

        [JsonPropertyName("position")]
        public int Position { get; set; }
    }
    public class AllMatchesTableOverall
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("cleanName")]
        public string CleanName { get; set; }

        [JsonPropertyName("points")]
        public int Points { get; set; }

        [JsonPropertyName("position")]
        public int Position { get; set; }

        [JsonPropertyName("seasonGoals")]
        public int SeasonGoals { get; set; }

        [JsonPropertyName("seasonConceded")]
        public int SeasonConceded { get; set; }

        [JsonPropertyName("matchesPlayed")]
        public int MatchesPlayed { get; set; }
    }
}
