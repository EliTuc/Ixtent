using System.Text.Json.Serialization;
using System.Collections;

namespace MatchHype.Api.DataProvider
{
    public class StatsTeamRoot
    {
        [JsonPropertyName("data")]
        public List<TeamData> Data { get; set; }
    }

    public class TeamData
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("full_name")]
        public string FullName { get; set; }

        [JsonPropertyName("english_name")]
        public string EnglishName { get; set; }

        [JsonPropertyName("alt_names")]
        public List<string> AltNames { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("stats")]
        public Stats Stats { get; set; }
    }

    public class Stats
    {
        [JsonPropertyName("additional_info")]
        public AddInfo AddInfo { get; set; }

        [JsonPropertyName("cardsHighest_overall")]
        public int CardsHighestOverall { get; set; }

        [JsonPropertyName("cardsLowest_overall")]
        public int CardsLowestOverall { get; set; }

        [JsonPropertyName("cardsAVG_overall")]
        public double CardsAVGOverall { get; set; }

        [JsonPropertyName("cardsAVG_home")]
        public double CardsAVGHome { get; set; }

        [JsonPropertyName("cardsAVG_away")]
        public double CardsAVGAway { get; set; }


    }
    public class AddInfo
    { 
        [JsonPropertyName("formRun_overall")]
        public string FormRunOverall { get; set; }

        public string M1_WDL { get; set; }
        public string M2_WDL { get; set; }
        public string M3_WDL { get; set; }
        public string M4_WDL { get; set; }
        public string M5_WDL { get; set; }

        public void ParseFormRunOverall()
        {
            if (!string.IsNullOrEmpty(FormRunOverall) && FormRunOverall.Length >= 5)
            {
                M1_WDL = FormRunOverall[4].ToString();
                M2_WDL = FormRunOverall[3].ToString();
                M3_WDL = FormRunOverall[2].ToString();
                M4_WDL = FormRunOverall[1].ToString();
                M5_WDL = FormRunOverall[0].ToString();
            }
        }

    }

}