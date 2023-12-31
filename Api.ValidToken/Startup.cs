using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Api.ValidToken.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Api.ValidToken
{
    public class Startup
    {
        public static IConfiguration StaticConfig { get; private set; }
        public static IConfiguration ConnectionConfig { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            StaticConfig = configuration.GetSection("Setting");
            ConnectionConfig = configuration.GetSection("ConnectionStrings");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ConfigurationModel>(Configuration.GetSection("Setting"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors();
            services.AddIdentityServer(x =>
            {
                x.IssuerUri = "none";
            })
                .AddDeveloperSigningCredential(persistKey: false)
                .AddInMemoryApiResources(Config.GetAllApiResources())
                .AddInMemoryClients(Config.GetClients(Configuration));
            services.AddCors();
            services.AddScoped<HttpClient>();

            var settings = Configuration.GetSection("ConnectionStrings").Get<Setting>();

            SQLHelper.SQLHandlerAsync.Connectionconfig = settings.DefaultConnection;
            SQLHelper.SQLHandler.Connectionconfig = settings.DefaultConnection;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseIdentityServer();
            app.UseCors(x => x
              .AllowAnyMethod()
              .AllowAnyHeader()
              .SetIsOriginAllowed(origin => true)  
               );
            app.UseMvc();
        }
    }
}
