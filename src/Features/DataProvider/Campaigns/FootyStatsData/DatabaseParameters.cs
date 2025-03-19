using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Microsoft.Extensions.Azure;

namespace MatchHype.Api.Features.DataProvider.Campaigns.FootyStatsData;


public class DatabaseParameters : ClassMap<EventsDbData>
{
    public DatabaseParameters()
    {
        Map(m => m.StadiumID).Name("StadiumID").Optional();
        Map(m => m.StadiumName).Name("StadiumName").Optional();
        Map(m => m.Capacity).Name("Capacity").Optional();
        Map(m => m.CityName).Name("CityName").Optional();
        Map(m => m.AT_Code_Name).Name("AT_Code_Name").Optional();
        Map(m => m.AT_H2H_W).Name("AT_H2H_W").Optional();
        Map(m => m.AT_Last_Match_Score).Name("AT_Last_Match_Score").Optional();
        Map(m => m.AT_Rank).Name("AT_Rank").Optional();
        Map(m => m.AT_WDL).Name("AT_WDL").Optional();
        Map(m => m.AT_Name).Name("AT_Name").Optional();
        Map(m => m.AT_ElevenLabs).Name("AT_ElevenLabs").Optional();
        Map(m => m.AT_Prediction).Name("AT_Prediction").Optional();
        Map(m => m.BO_AT_Win).Name("BO_AT_Win").Optional();
        Map(m => m.BO_HT_Win).Name("BO_HT_Win").Optional();
        Map(m => m.BO_Both_teams_Draw).Name("BO_Both_teams_Draw").Optional();
        Map(m => m.Draw_Prediction).Name("Draw_Prediction").Optional();
        Map(m => m.MoreThanTwoGoals).Name("MoreThanTwoGoals").Optional();
        Map(m => m.LessThanTwoGoals).Name("LessThanTwoGoals").Optional();
        Map(m => m.HT_AT_Draw).Name("HT_AT_Draw").Optional();
        Map(m => m.HT_Code_Name).Name("HT_Code_Name").Optional();
        Map(m => m.HT_H2H_W).Name("HT_H2H_W").Optional();
        Map(m => m.HT_Last_Match_Score).Name("HT_Last_Match_Score").Optional();
        Map(m => m.HT_Rank).Name("HT_Rank").Optional();
        Map(m => m.Last_Match_Date).Name("Last_Match_Date").Optional();
        Map(m => m.HT_WDL).Name("HT_WDL").Optional();
        Map(m => m.HT_Name).Name("HT_Name").Optional();
        Map(m => m.HT_ElevenLabs).Name("HT_ElevenLabs").Optional();
        Map(m => m.ChampionshipID).Name("ChampionshipID").Optional();
        Map(m => m.League_Name).Name("League_Name").Optional();
        Map(m => m.Operator).Name("Operator").Optional();
        Map(m => m.HT_Prediction).Name("HT_Prediction").Optional();
        Map(m => m.CityElevenLabs).Name("CityElevenLabs").Optional();
        Map(m => m.MatchDate).Name("MatchDate").Optional();
        Map(m => m.Time).Name("Time").Optional();
        Map(m => m.SongVar).Name("SongVar").Optional();
        Map(m => m.Standings).Name("Standings").Optional();
        Map(m => m.Language).Name("Language").Optional();
        Map(m => m.VO_01).Name("VO_01").Optional();
        Map(m => m.VO_02).Name("VO_02").Optional();
        Map(m => m.VO_03).Name("VO_03").Optional();
        Map(m => m.VO_04).Name("VO_04").Optional();
        Map(m => m.VO_05).Name("VO_05").Optional();
        Map(m => m.VO_06).Name("VO_06").Optional();
        Map(m => m.VO_07).Name("VO_07").Optional();
    }
} 

public class EventsDbData
{
    public string StadiumID { get; set; }
    public string StadiumName { get; set; }
    public string Capacity { get; set; }
    public string CityName { get; set; }
    public string AT_Code_Name { get; set; }
    public string AT_H2H_W { get; set; }
    public string AT_Last_Match_Score { get; set; }
    public string AT_Rank { get; set; }
    public string AT_WDL { get; set; }
    public string AT_Name { get; set; }
    public string AT_ElevenLabs { get; set; }
    public string AT_ElevenLabs_EN { get; set; }  
    public string CityElevenLabs { get; set; }
    public string AT_Prediction { get; set; }
    public string BO_AT_Win { get; set; }
    public string BO_HT_Win { get; set; }
    public string BO_Both_teams_Draw { get; set; }
    public string Draw_Prediction { get; set; }
    public string MoreThanTwoGoals { get; set; }
    public string LessThanTwoGoals { get; set; }
    public string HT_AT_Draw { get; set; }
    public string HT_Code_Name { get; set; }
    public string HT_H2H_W { get; set; }
    public string HT_Last_Match_Score { get; set; }
    public string HT_Rank { get; set; }
    public string Last_Match_Date { get; set; }
    public string HT_WDL { get; set; }
    public string HT_Name { get; set; }
    public string HT_ElevenLabs { get; set; }
    public string HT_ElevenLabs_EN { get; set; }
    public string HT_Last_Tournament { get; set; }
    public string HT_Prediction { get; set; }
    public string MatchDate { get; set; }
    public string Time { get; set; }
    public string ChampionshipID { get; set; }
    public string Operator { get; set; }
    public string League_Name { get; set; }
    public string SongVar { get; set; }
    public string Standings { get; set; }
    public string Language { get; set; }
    public string VO_01 { get; set; }
    public string VO_02 { get; set; }
    public string VO_03 { get; set; }
    public string VO_04 { get; set; }
    public string VO_05 { get; set; }
    public string VO_06 { get; set; }
    public string VO_07 { get; set; }
}