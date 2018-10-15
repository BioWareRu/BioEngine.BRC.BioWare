using BioEngine.Core.Entities;
using BioEngine.Core.Site;
using BioEngine.Core.Web;
using Microsoft.AspNetCore.Mvc;

namespace BioEngine.BRC.BioWare.Controllers
{
    [Route("/")]
    public class ContentController : ContentItemController<ContentItem, int>
    {
        public ContentController(BaseControllerContext<ContentItem, int> context) : base(context)
        {
        }
    }
}