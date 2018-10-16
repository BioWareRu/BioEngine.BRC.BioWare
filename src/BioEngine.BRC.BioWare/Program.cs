using BioEngine.BRC.Domain.Entities;
using BioEngine.Core;
using BioEngine.Core.Infra;
using BioEngine.Core.Site;
using BioEngine.Extra.IPB;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace BioEngine.BRC.BioWare
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .AddBioEngineModule<CoreModule, CoreModuleConfig>(config =>
                {
                    config.Assemblies.Add(typeof(Post).Assembly);
                    config.EnableValidation = true;
                    config.MigrationsAssembly = typeof(Post).Assembly;
                })
                .AddBioEngineModule<InfraModule>()
                .AddBioEngineModule<IPBSiteModule>()
                .AddBioEngineModule<SiteModule>()
                .UseStartup<Startup>();
    }
}