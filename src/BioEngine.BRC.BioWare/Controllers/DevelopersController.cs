using BioEngine.BRC.Domain.Entities;
using BioEngine.Core.Site;
using BioEngine.Core.Web;
using Microsoft.AspNetCore.Mvc;

namespace BioEngine.BRC.BioWare.Controllers
{
    [Route("/developers")]
    public class DevelopersController : SiteController<Developer, int>
    {
        public DevelopersController(BaseControllerContext<Developer, int> context) : base(context)
        {
        }
    }
}