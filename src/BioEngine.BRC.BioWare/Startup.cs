﻿using BioEngine.BRC.Site;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BioEngine.BRC.BioWare
{
    public class Startup : BrcSiteStartup
    {
        public Startup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
        {
        }
    }
}
