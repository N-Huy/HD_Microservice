using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HDB_APIGETWAY
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var configApp = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", optional: false)
              .Build();
            ;
            var urlPort = configApp.GetSection("UrlPort").Value;
            return WebHost.CreateDefaultBuilder(args)
              .ConfigureKestrel(serverOptions =>
              {
              })
                .ConfigureAppConfiguration((host, config) =>
                {
                    config.AddJsonFile(Path.Combine("configuration", "configuration.json"));
                })
                .UseStartup<Startup>().UseUrls(urlPort);
        } 
    }
}
