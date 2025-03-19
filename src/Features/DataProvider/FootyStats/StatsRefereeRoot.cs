using System.Text.Json.Serialization;
using System.Collections;

namespace MatchHype.Api.DataProvider
{
    public class StatsRefereeRoot
    {
        [JsonPropertyName("data")]
        public List<RefData> Data { get; set; }
    }

    public class RefData
    {
        [JsonPropertyName("competition_id")]
        public int CompetitionId { get; set; }

        [JsonPropertyName("full_name")]
        public string FullName { get; set; }

        [JsonPropertyName("league")]
        public string LeagueName { get; set; }

        [JsonPropertyName("cards_per_match_overall")]
        public double CardsPerMatchOverallReferee { get; set; }

    }

}