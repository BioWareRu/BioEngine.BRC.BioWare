using BioEngine.BRC.Domain.Entities;
using BioEngine.Core;
using BioEngine.Core.Infra;
using BioEngine.Core.Site;
using BioEngine.Extra.Ads;
using BioEngine.Extra.Ads.Entities;
using BioEngine.Extra.IPB;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BioEngine.BRC.BioWare
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        [PublicAPI]
        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .AddBioEngineModule<CoreModule, CoreModuleConfig>(config =>
                {
                    config.Assemblies.Add(typeof(Developer).Assembly);
                    config.Assemblies.Add(typeof(Ad).Assembly);
                    config.EnableValidation = true;
                    config.MigrationsAssembly = typeof(Developer).Assembly;
                    config.EnableElasticSearch = true;
                })
                .AddBioEngineModule<InfraModule>()
                .AddBioEngineModule<IPBSiteModule>()
                .AddBioEngineModule<SiteModule>()
                .AddBioEngineModule<AdsModule>()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
