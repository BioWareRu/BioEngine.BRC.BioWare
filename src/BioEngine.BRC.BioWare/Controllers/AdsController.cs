using BioEngine.Extra.Ads.Entities;
using BioEngine.Extra.Ads.Site;
using Microsoft.AspNetCore.Mvc;

namespace BioEngine.BRC.BioWare.Controllers
{
    [Route("ads")]
    public class AdsController : AdsSiteController
    {
        public AdsController(AdsRepository adsRepository) : base(adsRepository)
        {
        }
    }
}
