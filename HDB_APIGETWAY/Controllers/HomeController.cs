using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HDB_APIGETWAY.Models;

using Newtonsoft.Json;
using SQLHelper;
using static HDB_APIGETWAY.Models.ModelMicroservice;

namespace HDB_APIGETWAY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("getreroutes")]
        [HttpGet]
        public OutputJS GetReRoutes()
        {
            OutputJS outputJS = new OutputJS();
            return outputJS;
            //    var result = new ConfigOutput();
            //    var lstReroute = new List<ReRoutes>();
            //    try
            //    {

            //        string strSpName = "Get_ReRoutes";
            //        SQLHandler sqlHAsy = new SQLHandler();
            //        var listData = sqlHAsy.ExecuteAsList<ReRoutesOutput>(strSpName);

            //        foreach (var data in listData)
            //        {
            //            List<KeyValuePair<string, object>> Param = new List<KeyValuePair<string, object>>();
            //            Param.Add(new KeyValuePair<string, object>("@ReRoutesId", data.Id));

            //            // Get DownstreamHostAndPorts

            //            string strSpNameDownstreamHostAndPorts = "Get_DownstreamHostAndPorts";
            //            var DownstreamHostAndPorts = sqlHAsy.ExecuteAsList<DownstreamHostAndPortsInput>(strSpNameDownstreamHostAndPorts, Param);

            //            // Get UpstreamHttpMethod

            //            string strSpNameUpstreamHttpMethod = "Get_UpstreamHttpMethod";
            //            var UpstreamHttpMethod = sqlHAsy.ExecuteAsList<UpstreamHttpMethodInput>(strSpNameUpstreamHttpMethod, Param);
            //            var dataUpstreamHttpMethod = new List<string>();
            //            foreach (var item in UpstreamHttpMethod)
            //            {
            //                dataUpstreamHttpMethod.Add(item.Name);
            //            }


            //            // Get AuthenticationOptions

            //            string strSpNameAuthenticationOptions = "Get_AuthenticationOptions";
            //            var AuthenticationOptions = sqlHAsy.ExecuteAsList<AuthenticationOptionsInput>(strSpNameAuthenticationOptions, Param).FirstOrDefault();

            //            //// Get AllowedScopes
            //            //List<KeyValuePair<string, object>> ParamAllowedScopes = new List<KeyValuePair<string, object>>();
            //            //ParamAllowedScopes.Add(new KeyValuePair<string, object>("@Id", AuthenticationOptions1.Id));
            //            //string strSpNameAllowedScopes = "Get_AllowedScopes";
            //            //var AllowedScopes = sqlHAsy.ExecuteAsList<AllowedScopesInput>(strSpNameUpstreamHttpMethod, ParamAllowedScopes);


            //            // Ins RateLimitOptions

            //            string strSpNameRateLimitOptions = "Get_RateLimitOptions";

            //            var RateLimitOptions = sqlHAsy.ExecuteAsList<RateLimitOptionsInput>(strSpNameRateLimitOptions, Param).FirstOrDefault();

            //            //// Get ClientWhitelist
            //            //List<KeyValuePair<string, object>> ParamClientWhitelist = new List<KeyValuePair<string, object>>();
            //            //ParamClientWhitelist.Add(new KeyValuePair<string, object>("@Id", RateLimitOptions1.Id));
            //            //string strSpNameClientWhitelist = "Get_AllowedScopes";
            //            //var ClientWhitelist = sqlHAsy.ExecuteAsList<AllowedScopesInput>(strSpNameUpstreamHttpMethod, ParamAllowedScopes);

            //            // Ins FileCacheOptions
            //            string strSpNameFileCacheOptions = "Get_FileCacheOptions";

            //            var FileCacheOptions = sqlHAsy.ExecuteAsList<FileCacheOptionsInput>(strSpNameFileCacheOptions, Param).FirstOrDefault();

            //            string JSONString = string.Empty;
            //            var reroute = new ReRoutes();
            //            reroute.DownstreamHostAndPorts = data.DownstreamHostAndPorts;
            //            reroute.DownstreamPathTemplate = data.DownstreamPathTemplate;
            //            reroute.UpstreamPathTemplate = data.UpstreamPathTemplate;
            //            reroute.UpstreamPathTemplate = data.UpstreamPathTemplate;
            //            reroute.DownstreamScheme = data.DownstreamScheme;


            //            reroute.DownstreamHostAndPorts = DownstreamHostAndPorts;
            //            reroute.UpstreamHttpMethod = dataUpstreamHttpMethod;
            //            reroute.AuthenticationOptions = AuthenticationOptions;
            //            reroute.RateLimitOptions = RateLimitOptions;
            //            reroute.FileCacheOptions = FileCacheOptions;
            //            reroute.AuthenticationOptions.AllowedScopes = new List<AllowedScopesInput>();
            //            reroute.RateLimitOptions.ClientWhitelist = new List<ClientWhitelistInput>();

            //            lstReroute.Add(reroute);
            //        }

            //        var globalconfig = new GlobalConfiguration();
            //        result.GlobalConfiguration = globalconfig;
            //        result.ReRoutes = lstReroute;

            //        string jsonData = JsonConvert.SerializeObject(result, Formatting.Indented);

            //        // Ghi dữ liệu vào configuration.json
            //        var templatePath = $"{Environment.CurrentDirectory}\\Configuration\\configuration.json";
            //        string configFilePath = "testjson.json";
            //        System.IO.File.WriteAllText(templatePath, jsonData);
            //        if (listData.Count > 0)
            //        {
            //            outputJS.status = 1;
            //            outputJS.result = result;

            //            outputJS.code = "00";
            //            outputJS.message = "Thành công";
            //            outputJS.total = 1;
            //            outputJS.page = 0;
            //        }
            //        else
            //        {
            //            outputJS.status = 1;
            //            outputJS.result = null;
            //            outputJS.code = "01";
            //            outputJS.message = "Thất bại";
            //            outputJS.total = 1;
            //            outputJS.page = 0;
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        outputJS.status = 0;
            //        outputJS.result = null;

            //        outputJS.code = "01";
            //        outputJS.message = ex.Message;
            //        outputJS.total = 0;
            //        outputJS.page = 0;
            //    }

            //    return result;
            //}
        }
    }
}
