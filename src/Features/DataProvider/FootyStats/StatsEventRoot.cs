using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace MatchHype.Api.DataProvider
{
    public class BettingOddsRoot
    {
        [JsonPropertyName("data")]
        public StatsEventData Data { get; set; }
    }

    public class StatsEventData
    {
        [JsonPropertyName("home_name")]
        public string HT_Name { get; set; }

        [JsonPropertyName("away_name")]
        public string AT_Name { get; set; }

        [JsonPropertyName("homeID")]
        public int HT_ID { get; set; }

        [JsonPropertyName("awayID")]
        public int AT_ID { get; set; }

        [JsonPropertyName("stadium_name")]
        public string StadiumName { get; set; }

        [JsonPropertyName("stadium_location")]
        public string StadiumLocation { get; set; }

        [JsonPropertyName("date_unix")]
        public int DateUnix { get; set; }

        [JsonPropertyName("competition_id")]
        public int ChampionshipID { get; set; }

        [JsonPropertyName("h2h")]
        public H2HData H2H { get; set; }

        [JsonPropertyName("odds_ft_1")]
        public double HT_Prediction { get; set; }

        [JsonPropertyName("odds_ft_x")]
        public double Draw_Prediction { get; set; }

        [JsonPropertyName("odds_ft_2")]
        public double AT_Prediction { get; set; }


        [JsonPropertyName("corners_potential")]
        public double CornersPotential { get; set; }

        [JsonPropertyName("cards_potential")]
        public double CardsPotential { get; set; }

        [JsonPropertyName("team_a_yellow_cards")]
        public int Team_A_Yellow_Cards { get; set; }

        [JsonPropertyName("team_b_yellow_cards")]
        public int Team_B_Yellow_Cards { get; set; }

        [JsonPropertyName("odds_ft_over15")]
        public double MoreThanTwoGoals { get; set; }

        [JsonPropertyName("odds_ft_under15")]
        public double LessThanTwoGoals { get; set; }

    }

    public class H2HData
    {
        [JsonPropertyName("previous_matches_ids")]
        public List<PreviousMatch> PreviousMatchesIds { get; set; }


    }

    public class PreviousMatch
    {
        [JsonPropertyName("id")]
        public int IdPreviousMatch { get; set; }

        [JsonPropertyName("date_unix")]
        public int DateUnixLast { get; set; }

        [JsonPropertyName("team_a_id")]
        public int Team_A_ID { get; set; }

        [JsonPropertyName("team_b_id")]
        public int Team_B_ID { get; set; }

        [JsonPropertyName("team_a_goals")]
        public int Team_A_Goals { get; set; }

        [JsonPropertyName("team_b_goals")]
        public int Team_B_Goals { get; set; }
    }

}