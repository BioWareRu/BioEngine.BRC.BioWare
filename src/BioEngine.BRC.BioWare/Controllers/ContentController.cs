using BioEngine.Core.Entities;
using BioEngine.Core.Site;
using Microsoft.AspNetCore.Mvc;

namespace BioEngine.BRC.BioWare.Controllers
{
    [Route("/")]
    [Route("/content")]
    public class ContentController : ContentItemController<Post, int>
    {
        public ContentController(SiteControllerContext<Post, int> context) : base(context)
        {
        }
    }
}