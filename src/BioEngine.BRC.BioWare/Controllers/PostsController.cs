using BioEngine.Core.Entities;
using BioEngine.Core.Site;
using Microsoft.AspNetCore.Mvc;

namespace BioEngine.BRC.BioWare.Controllers
{
    [Route("/")]
    [Route("post")]
    public class PostsController : ContentItemController<Post>
    {
        public PostsController(SiteControllerContext<Post> context) : base(context)
        {
        }
    }
}
