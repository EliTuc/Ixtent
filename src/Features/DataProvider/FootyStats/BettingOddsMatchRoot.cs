using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MatchHype.Api.Features.DataProvider.FootyStats
{
    public class BettingOddsMatchRoot
    {
        [JsonPropertyName("Markets")]
        public List<Market> Markets { get; set; }
    }

    public class Market

    {

        [JsonPropertyName("Name")]
        public string OddsTypeName { get; set; }

        [JsonPropertyName("Selections")]
        public List<Selection> Selections { get; set; }
    }

    public class Selection
    {
        [JsonPropertyName("SelectionId")]
        public long SelectionId { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Price")]
        public string Price { get; set; }

    }

}
