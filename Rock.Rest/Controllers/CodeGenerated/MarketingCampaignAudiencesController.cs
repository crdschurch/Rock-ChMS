//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the Rock.CodeGeneration project
//     Changes to this file will be lost when the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
//
// THIS WORK IS LICENSED UNDER A CREATIVE COMMONS ATTRIBUTION-NONCOMMERCIAL-
// SHAREALIKE 3.0 UNPORTED LICENSE:
// http://creativecommons.org/licenses/by-nc-sa/3.0/
//

using Rock.Model;

namespace Rock.Rest.Controllers
{
    /// <summary>
    /// MarketingCampaignAudiences REST API
    /// </summary>
    public partial class MarketingCampaignAudiencesController : Rock.Rest.ApiController<Rock.Model.MarketingCampaignAudience, Rock.Model.MarketingCampaignAudienceDto>
    {
        public MarketingCampaignAudiencesController() : base( new Rock.Model.MarketingCampaignAudienceService() ) { } 
    }
}