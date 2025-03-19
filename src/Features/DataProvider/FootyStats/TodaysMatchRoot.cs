using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace MatchHype.Api.DataProvider;

public class TodaysMatchRoot
{
    [JsonPropertyName("data")]
    public List<TodaysMatchData> Data { get; set; }
}

public class TodaysMatchData
{
    [JsonPropertyName("id")]
    public int MatchId { get; set; }

    [JsonPropertyName("competition_id")]
    public int ChampionshipId { get; set; }

    [JsonPropertyName("refereeID")]
    public int? RefereeId { get; set; }


}