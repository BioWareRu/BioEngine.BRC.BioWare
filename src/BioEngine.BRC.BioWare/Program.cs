using System;
using BioEngine.BRC.Domain;
using BioEngine.Core.DB;
using BioEngine.Core.Logging.Loki;
using BioEngine.Core.Search.ElasticSearch;
using BioEngine.Core.Seo;
using BioEngine.Core.Site;
using BioEngine.Core.Storage;
using BioEngine.Extra.Ads;
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
            new Core.BioEngine(args)
                .AddModule<PostgresDatabaseModule, PostgresDatabaseModuleConfig>(
                    (config, configuration, env) =>
                    {
                        config.Host = configuration["BE_POSTGRES_HOST"];
                        config.Port = int.Parse(configuration["BE_POSTGRES_PORT"]);
                        config.Username = configuration["BE_POSTGRES_USERNAME"];
                        config.Password = configuration["BE_POSTGRES_PASSWORD"];
                        config.Database = configuration["BE_POSTGRES_DATABASE"];
                        config.EnablePooling = env.IsDevelopment();
                    })
                .AddModule<BrcDomainModule>()
                .AddModule<LokiLoggingModule, LokiLoggingConfig>((config, configuration, env) =>
                {
                    config.Url = configuration["BRC_LOKI_URL"];
                })
                .AddModule<ElasticSearchModule, ElasticSearchModuleConfig>(
                    (config, configuration, env) =>
                    {
                        config.Url = configuration["BE_ELASTICSEARCH_URI"];
                        config.Login = configuration["BE_ELASTICSEARCH_LOGIN"];
                        config.Password = configuration["BE_ELASTICSEARCH_PASSWORD"];
                    })
                .AddModule<S3StorageModule, S3StorageModuleConfig>((config, configuration, env) =>
                {
                    var uri = configuration["BE_STORAGE_PUBLIC_URI"];
                    var success = Uri.TryCreate(uri, UriKind.Absolute, out var publicUri);
                    if (!success)
                    {
                        throw new ArgumentException($"URI {uri} is not proper URI");
                    }

                    var serverUriStr = configuration["BE_STORAGE_S3_SERVER_URI"];
                    success = Uri.TryCreate(serverUriStr, UriKind.Absolute, out var serverUri);
                    if (!success)
                    {
                        throw new ArgumentException($"S3 server URI {uri} is not proper URI");
                    }

                    config.PublicUri = publicUri;
                    config.ServerUri = serverUri;
                    config.Bucket = configuration["BE_STORAGE_S3_BUCKET"];
                    config.AccessKey = configuration["BE_STORAGE_S3_ACCESS_KEY"];
                    config.SecretKey = configuration["BE_STORAGE_S3_SECRET_KEY"];
                })
                .AddModule<SeoModule>()
                .AddModule<IPBSiteModule, IPBModuleConfig>((config, configuration, env) =>
                {
                    if (!Uri.TryCreate(configuration["BE_IPB_URL"], UriKind.Absolute, out var ipbUrl))
                    {
                        throw new ArgumentException($"Can't parse IPB url; {configuration["BE_IPB_URL"]}");
                    }

                    config.Url = ipbUrl;
                    config.ApiClientId = configuration["BE_IPB_OAUTH_CLIENT_ID"];
                    config.ApiClientSecret = configuration["BE_IPB_OAUTH_CLIENT_SECRET"];
                    config.CallbackPath = configuration["BE_IPB_CALLBACK_PATH"];
                    config.AuthorizationEndpoint = configuration["BE_IPB_AUTHORIZATION_ENDPOINT"];
                    config.TokenEndpoint = configuration["BE_IPB_TOKEN_ENDPOINT"];
                })
                .AddModule<SiteModule, SiteModuleConfig>((config, configuration, env) =>
                {
                    config.SiteId = Guid.Parse(configuration["BE_SITE_ID"]);
                })
                .AddModule<AdsModule>()
                .GetHostBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
