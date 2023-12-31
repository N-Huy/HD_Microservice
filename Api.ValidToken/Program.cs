﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Api.ValidToken
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: false)
             .Build();
            ;
            var urlPort = config.GetSection("UrlPort").Value;
            return WebHost.CreateDefaultBuilder(args)
             .ConfigureKestrel(serverOptions =>
                {
                })
                 .UseStartup<Startup>().UseUrls(urlPort);

        }
    }
}
