using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace MatchHype.Api.DataProvider;
public class JsonTeamData
{
    [JsonPropertyName("code")]
    public string CodeName { get; set; }

    [JsonPropertyName("name")]
    public string TeamName { get; set; }

    [JsonPropertyName("elevenLabsInput")]
    public string ElevenLabsInput { get; set; }

    [JsonPropertyName("elevenLabsInput_EN")]
    public string ElevenLabsInputEN { get; set; }

    [JsonPropertyName("short_name")]
    public string ShortName { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("venue_id")]
    public int VenueId { get; set; }

    [JsonPropertyName("venue")]
    public string Venue { get; set; }

    [JsonPropertyName("capacity")]
    public int Capacity { get; set; }

    [JsonPropertyName("cityEN")]
    public string CityEN { get; set; }

    [JsonPropertyName("cityCZ")]
    public string CityCZ { get; set; }

    [JsonPropertyName("cityElevenLabsCZ")]
    public string CityElevenLabsCZ { get; set; }

    [JsonPropertyName("cityElevenLabsEN")]
    public string CityElevenLabsEN { get; set; }
}
