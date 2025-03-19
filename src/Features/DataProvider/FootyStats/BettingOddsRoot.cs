using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MatchHype.Api.Features.DataProvider.FootyStats
{
    public class AltenarBettingOddsRoot
    {
        [JsonPropertyName("Categories")]
        public List<Category> Categories { get; set; }
    }

    public class Category
    {
        [JsonPropertyName("CategoryId")]
        public int CategoryId { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Championships")]
        public List<Championship> Championships { get; set; }
    }

    public class Championship
    {
        [JsonPropertyName("ChampionshipId")]
        public int ChampionshipId { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Events")]
        public List<EventAltenar> Events { get; set; }
    }

    public class EventAltenar
    {
        [JsonPropertyName("EventId")]
        public int EventId { get; set; }

        [JsonPropertyName("EventName")]
        public string EventName { get; set; }

        [JsonPropertyName("EventType")]
        public string EventType { get; set; }

        [JsonPropertyName("EventDate")]
        public DateTime EventDate { get; set; }

        [JsonPropertyName("ExtId")]
        public string ExtId { get; set; }
    }
}
