using BioEngine.BRC.Site;
using Microsoft.Extensions.Configuration;

namespace BioEngine.BRC.BioWare
{
    public class Startup : BrcSiteStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
