using BioEngine.Core.Entities;
using BioEngine.Core.Site;
using Microsoft.AspNetCore.Mvc;

namespace BioEngine.BRC.BioWare.Controllers
{
    [Route("/")]
    [Route("/content")]
    public class ContentController : ContentItemController<ContentItem, int>
    {
        public ContentController(SiteControllerContext<ContentItem, int> context) : base(context)
        {
        }
    }
}