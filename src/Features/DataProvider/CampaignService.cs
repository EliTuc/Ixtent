using Microsoft.AspNetCore.Mvc;

namespace MatchHype.Api.Features.DataProvider
{
    public class CampaignService
    {
         
        public async Task<IActionResult> GenerateRoundInitialCsv(string campaignName, string matchId, string language)
        {

            switch (campaignName)
            {

                case "FootyStatsData":
                    var theSportsDbService5 = new Campaigns.FootyStatsData.TheSportsDbService();
                        return await theSportsDbService5.GenerateRoundInitialCsv(matchId, language);


                default:
                    return new BadRequestObjectResult("Invalid campaign name");

            }
        }

    }
}
