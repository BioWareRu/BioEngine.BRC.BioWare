using BioEngine.BRC.Domain.Entities;
using BioEngine.Core.Site;
using Microsoft.AspNetCore.Mvc;

namespace BioEngine.BRC.BioWare.Controllers
{
    [Route("/developers")]
    public class DevelopersController : SiteController<Developer>
    {
        public DevelopersController(SiteControllerContext<Developer> context) : base(context)
        {
        }
    }
}
