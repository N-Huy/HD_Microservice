using System.Collections.Generic;
using System.Data.SqlClient;
using Api.ValidToken.Controllers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Api.ValidToken
{
    public static class Config
    {

        public static IEnumerable<ApiResource> GetAllApiResources()
        {

            var data = MicroserviceController.SQLGetAllApiResources();
            return data;
            //return new List<ApiResource>
            //{
            //    new ApiResource("QLHCSCOPE","HDBANK MOBILE"),
            //    new ApiResource("QLHC.PTVC","HDBANK SCOPE") ,
            //    new ApiResource("HDBANKSCOPE","HDBANK SCOPE"),
            //    new ApiResource("TESTSCOPE","HDBANK SCOPE")
            //};
        }

        public static IEnumerable<Client> GetClients([FromServices]IConfiguration configuration)
        {
            string clSecrets = Startup.StaticConfig.GetSection("ClientSecrets").Value;
            string allowScope = Startup.StaticConfig.GetSection("AllowedScopes").Value;
            string clId = Startup.StaticConfig.GetSection("ClientId").Value;

            var data = MicroserviceController.SQLGetAllowedScopes();
            return new List<Client>
            {
                new Client
                {
                    ClientId = clId,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret(clSecrets.Sha256())
                    },
                    AllowedScopes = data,
                    AccessTokenLifetime =28800,
                   
                }
            };
        }
    }
}
