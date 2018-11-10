using BioEngine.BRC.Domain.Entities;
using BioEngine.Core;
using BioEngine.Core.Entities.Blocks;
using BioEngine.Core.Infra;
using BioEngine.Core.Site;
using BioEngine.Extra.IPB;
using JetBrains.Annotations;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace BioEngine.BRC.BioWare
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        [PublicAPI]
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .AddBioEngineModule<CoreModule, CoreModuleConfig>(config =>
                {
                    config.Assemblies.Add(typeof(Developer).Assembly);
                    config.EnableValidation = true;
                    config.MigrationsAssembly = typeof(Developer).Assembly;
                })
                .AddBioEngineModule<InfraModule>()
                .AddBioEngineModule<IPBSiteModule>()
                .AddBioEngineModule<SiteModule>()
                .UseStartup<Startup>();
    }
}