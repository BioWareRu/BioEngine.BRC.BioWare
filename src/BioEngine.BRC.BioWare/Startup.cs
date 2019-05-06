using System;
using BioEngine.BRC.BioWare.Patreon;
using BioEngine.Core.Site;
using BioEngine.Extra.IPB.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BioEngine.BRC.BioWare
{
    public class Startup : BioEngineStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services.AddSingleton<PatreonApiHelper>();
            services.Configure<PatreonConfig>(o =>
            {
                o.ServiceUrl = new Uri(Configuration["BE_PATREON_SERVICE_URL"]);
            });
            services.AddIpbOauthAuthentication(Configuration);
        }

        protected override void ConfigureApp(IApplicationBuilder app, IHostingEnvironment env)
        {
            base.ConfigureApp(app, env);

            app.UseAuthentication();
        }
    }
}
