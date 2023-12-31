using HDB_APIGETWAY.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SQLHelper;
using static HDB_APIGETWAY.Models.ModelMicroservice;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace HDB_APIGETWAY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MicroserviceController : Controller
    {
        [HttpPost("insert")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<OutputJS> Test(ReRoutesInput input)
        {
            OutputJS outputJS = new OutputJS();
            try
            {
                // Ins reroutes 
                List<KeyValuePair<string, object>> Param = new List<KeyValuePair<string, object>>();
                Param.Add(new KeyValuePair<string, object>("@DownstreamPathTemplate", input.DownstreamPathTemplate));
                Param.Add(new KeyValuePair<string, object>("@DownstreamScheme", input.DownstreamScheme));
                Param.Add(new KeyValuePair<string, object>("@UpstreamPathTemplate", input.UpstreamPathTemplate));

                var jsonParams = JsonConvert.SerializeObject(input);

                string strSpName = "Ins_ReRoute";
                SQLHandler sqlHAsy = new SQLHandler();
                var listData = sqlHAsy.ExecuteAsList<ReRoutesOutput>(strSpName, Param);


                // Ins DownstreamHostAndPorts
                var DownstreamHostAndPorts = new List<DownstreamHostAndPortsInput>();
                foreach (var item in input.DownstreamHostAndPorts)
                {
                    List<KeyValuePair<string, object>> ParamDownstreamHostAndPorts = new List<KeyValuePair<string, object>>();
                    ParamDownstreamHostAndPorts.Add(new KeyValuePair<string, object>("@ReRoutesId", listData.FirstOrDefault().Id));
                    ParamDownstreamHostAndPorts.Add(new KeyValuePair<string, object>("@Host", item.Host));
                    ParamDownstreamHostAndPorts.Add(new KeyValuePair<string, object>("@Port", item.Post));

                    string strSpNameDownstreamHostAndPorts = "Ins_DownstreamHostAndPorts";

                    DownstreamHostAndPorts = sqlHAsy.ExecuteAsList<DownstreamHostAndPortsInput>(strSpNameDownstreamHostAndPorts, ParamDownstreamHostAndPorts);
                }

                // Ins UpstreamHttpMethod
                var UpstreamHttpMethod = new List<UpstreamHttpMethodInput>();
                foreach (var item in input.UpstreamHttpMethod)
                {
                    List<KeyValuePair<string, object>> ParamUpstreamHttpMethod = new List<KeyValuePair<string, object>>();
                    ParamUpstreamHttpMethod.Add(new KeyValuePair<string, object>("@ReRoutesId", listData.FirstOrDefault().Id));
                    ParamUpstreamHttpMethod.Add(new KeyValuePair<string, object>("@Name", item.Name));

                    string strSpNameUpstreamHttpMethod = "Ins_UpstreamHttpMethod";

                    UpstreamHttpMethod = sqlHAsy.ExecuteAsList<UpstreamHttpMethodInput>(strSpNameUpstreamHttpMethod, ParamUpstreamHttpMethod);
                }

                var dataUpstreamHttpMethod = new List<string>();
                foreach (var item in UpstreamHttpMethod)
                {
                    dataUpstreamHttpMethod.Add(item.Name);
                }

                // Ins AuthenticationOptions

                List<KeyValuePair<string, object>> ParamAuthenticationOptions = new List<KeyValuePair<string, object>>();
                ParamAuthenticationOptions.Add(new KeyValuePair<string, object>("@ReRoutesId", listData.FirstOrDefault().Id));
                ParamAuthenticationOptions.Add(new KeyValuePair<string, object>("@AuthenticationProviderKey", input.AuthenticationOptions.AuthenticationProviderKey));

                string strSpNameAuthenticationOptions = "Ins_AuthenticationOptions";

                var AuthenticationOptions = sqlHAsy.ExecuteAsList<AuthenticationOptionsInput>(strSpNameAuthenticationOptions, ParamAuthenticationOptions);

                // Ins RateLimitOptions

                List<KeyValuePair<string, object>> ParamRateLimitOptions = new List<KeyValuePair<string, object>>();
                ParamRateLimitOptions.Add(new KeyValuePair<string, object>("@ReRoutesId", listData.FirstOrDefault().Id));
                ParamRateLimitOptions.Add(new KeyValuePair<string, object>("@EnableRateLimiting", input.RateLimitOptions.EnableRateLimiting));
                ParamRateLimitOptions.Add(new KeyValuePair<string, object>("@Period", input.RateLimitOptions.Period));
                ParamRateLimitOptions.Add(new KeyValuePair<string, object>("@PeriodTimespan", input.RateLimitOptions.PeriodTimespan));
                ParamRateLimitOptions.Add(new KeyValuePair<string, object>("@Limit", input.RateLimitOptions.Limit));

                string strSpNameRateLimitOptions = "Ins_RateLimitOptions";

                var RateLimitOptions = sqlHAsy.ExecuteAsList<RateLimitOptionsInput>(strSpNameRateLimitOptions, ParamRateLimitOptions);


                // Ins FileCacheOptions

                List<KeyValuePair<string, object>> ParamFileCacheOptions = new List<KeyValuePair<string, object>>();
                ParamFileCacheOptions.Add(new KeyValuePair<string, object>("@ReRoutesId", listData.FirstOrDefault().Id));
                ParamFileCacheOptions.Add(new KeyValuePair<string, object>("@TtlSeconds", input.FileCacheOptions.TtlSeconds));
                ParamFileCacheOptions.Add(new KeyValuePair<string, object>("@Region", input.FileCacheOptions.Region));

                string strSpNameFileCacheOptions = "Ins_FileCacheOptions";

                var FileCacheOptions = sqlHAsy.ExecuteAsList<FileCacheOptionsInput>(strSpNameFileCacheOptions, ParamFileCacheOptions);


                string JSONString = string.Empty;
                var result = listData.FirstOrDefault();
                result.DownstreamHostAndPorts = DownstreamHostAndPorts;
                result.UpstreamHttpMethod = dataUpstreamHttpMethod;
                result.AuthenticationOptions = AuthenticationOptions.FirstOrDefault();
                result.RateLimitOptions = RateLimitOptions.FirstOrDefault();
                result.FileCacheOptions = FileCacheOptions.FirstOrDefault();

                if (listData.Count > 0)
                {
                    outputJS.status = 1;
                    outputJS.result = listData.FirstOrDefault();

                    outputJS.code = "00";
                    outputJS.message = "Thành công";
                    outputJS.total = 1;
                    outputJS.page = 0;
                }
                else
                {
                    outputJS.status = 1;
                    outputJS.result = null;
                    outputJS.code = "01";
                    outputJS.message = "Thất bại";
                    outputJS.total = 1;
                    outputJS.page = 0;
                }

            }
            catch (Exception ex)
            {
                outputJS.status = 0;
                outputJS.result = null;

                outputJS.code = "01";
                outputJS.message = ex.Message;
                outputJS.total = 0;
                outputJS.page = 0;
            }

            return outputJS;
        }

        [HttpPost("getreroutes")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<ConfigOutput> GetReRoutes()
        {
            OutputJS outputJS = new OutputJS();
            var result = new ConfigOutput();
            var lstReroute = new List<ReRoutes>();
            try
            {

                string strSpName = "Get_ReRoutes";
                SQLHandler sqlHAsy = new SQLHandler();
                var listData = sqlHAsy.ExecuteAsList<ReRoutesOutput>(strSpName);

                foreach (var data in listData)
                {
                    List<KeyValuePair<string, object>> Param = new List<KeyValuePair<string, object>>();
                    Param.Add(new KeyValuePair<string, object>("@ReRoutesId", data.Id));

                    // Get DownstreamHostAndPorts

                    string strSpNameDownstreamHostAndPorts = "Get_DownstreamHostAndPorts";
                    var DownstreamHostAndPorts = sqlHAsy.ExecuteAsList<DownstreamHostAndPortsInput>(strSpNameDownstreamHostAndPorts, Param);

                    // Get UpstreamHttpMethod

                    string strSpNameUpstreamHttpMethod = "Get_UpstreamHttpMethod";
                    var UpstreamHttpMethod = sqlHAsy.ExecuteAsList<UpstreamHttpMethodInput>(strSpNameUpstreamHttpMethod, Param);
                    var dataUpstreamHttpMethod = new List<string>();
                    foreach (var item in UpstreamHttpMethod)
                    {
                        dataUpstreamHttpMethod.Add(item.Name);
                    }


                    // Get AuthenticationOptions

                    string strSpNameAuthenticationOptions = "Get_AuthenticationOptions";
                    var AuthenticationOptions = sqlHAsy.ExecuteAsList<AuthenticationOptionsInput>(strSpNameAuthenticationOptions, Param).FirstOrDefault();

                    //// Get AllowedScopes
                    //List<KeyValuePair<string, object>> ParamAllowedScopes = new List<KeyValuePair<string, object>>();
                    //ParamAllowedScopes.Add(new KeyValuePair<string, object>("@Id", AuthenticationOptions1.Id));
                    //string strSpNameAllowedScopes = "Get_AllowedScopes";
                    //var AllowedScopes = sqlHAsy.ExecuteAsList<AllowedScopesInput>(strSpNameUpstreamHttpMethod, ParamAllowedScopes);


                    // Ins RateLimitOptions

                    string strSpNameRateLimitOptions = "Get_RateLimitOptions";

                    var RateLimitOptions = sqlHAsy.ExecuteAsList<RateLimitOptionsInput>(strSpNameRateLimitOptions, Param).FirstOrDefault();

                    //// Get ClientWhitelist
                    //List<KeyValuePair<string, object>> ParamClientWhitelist = new List<KeyValuePair<string, object>>();
                    //ParamClientWhitelist.Add(new KeyValuePair<string, object>("@Id", RateLimitOptions1.Id));
                    //string strSpNameClientWhitelist = "Get_AllowedScopes";
                    //var ClientWhitelist = sqlHAsy.ExecuteAsList<AllowedScopesInput>(strSpNameUpstreamHttpMethod, ParamAllowedScopes);

                    // Ins FileCacheOptions
                    string strSpNameFileCacheOptions = "Get_FileCacheOptions";

                    var FileCacheOptions = sqlHAsy.ExecuteAsList<FileCacheOptionsInput>(strSpNameFileCacheOptions, Param).FirstOrDefault();

                    string JSONString = string.Empty;
                    var reroute = new ReRoutes();
                    reroute.DownstreamHostAndPorts = data.DownstreamHostAndPorts;
                    reroute.DownstreamPathTemplate = data.DownstreamPathTemplate;
                    reroute.UpstreamPathTemplate = data.UpstreamPathTemplate;
                    reroute.UpstreamPathTemplate = data.UpstreamPathTemplate;
                    reroute.DownstreamScheme = data.DownstreamScheme;


                    reroute.DownstreamHostAndPorts = DownstreamHostAndPorts;
                    reroute.UpstreamHttpMethod = dataUpstreamHttpMethod;
                    reroute.AuthenticationOptions = AuthenticationOptions;
                    reroute.RateLimitOptions = RateLimitOptions;
                    reroute.FileCacheOptions = FileCacheOptions;
                    reroute.AuthenticationOptions.AllowedScopes = new List<AllowedScopesInput>();
                    reroute.RateLimitOptions.ClientWhitelist = new List<ClientWhitelistInput>();

                    lstReroute.Add(reroute);
                }

                var globalconfig = new GlobalConfiguration();
                result.GlobalConfiguration = globalconfig;
                result.ReRoutes = lstReroute;

                string jsonData = JsonConvert.SerializeObject(result, Formatting.Indented);

                // Ghi dữ liệu vào configuration.json
                var templatePath = $"{Environment.CurrentDirectory}\\Configuration\\configuration.json";
                string configFilePath = "configuration.json";
                System.IO.File.WriteAllText(templatePath, jsonData);
                if (listData.Count > 0)
                {
                    outputJS.status = 1;
                    outputJS.result = result;

                    outputJS.code = "00";
                    outputJS.message = "Thành công";
                    outputJS.total = 1;
                    outputJS.page = 0;
                }
                else
                {
                    outputJS.status = 1;
                    outputJS.result = null;
                    outputJS.code = "01";
                    outputJS.message = "Thất bại";
                    outputJS.total = 1;
                    outputJS.page = 0;
                }

            }
            catch (Exception ex)
            {
                outputJS.status = 0;
                outputJS.result = null;

                outputJS.code = "01";
                outputJS.message = ex.Message;
                outputJS.total = 0;
                outputJS.page = 0;
            }

            return result;
        }

        [HttpPost("getappsettinggateway")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<OutputJS> GetAppsettingGateway(GetAppsettingInput input)
        {
            OutputJS outputJS = new OutputJS();

            var result = new AppsettingGatewayOutput();
            try
            {
                List<KeyValuePair<string, object>> Param = new List<KeyValuePair<string, object>>();
                Param.Add(new KeyValuePair<string, object>("@AllowedScopes", input.AllowedScopes));

                var jsonParams = JsonConvert.SerializeObject(input);

                string strSpName = "Get_Appsetting";
                SQLHandler sqlHAsy = new SQLHandler();
                var listData = sqlHAsy.ExecuteAsObject<AppsettingGateway>(strSpName, Param);
                string JSONString = string.Empty;


                List<KeyValuePair<string, object>> ParamConnectionStrings = new List<KeyValuePair<string, object>>();
                ParamConnectionStrings.Add(new KeyValuePair<string, object>("@AppsettingId", listData.Id));
                string strSpNameConnectionStrings = "Get_ConnectionStrings";
                var dataConnectionStrings = sqlHAsy.ExecuteAsObject<ConnectionStringsInput>(strSpNameConnectionStrings, ParamConnectionStrings);

                List<KeyValuePair<string, object>> ParamLogging = new List<KeyValuePair<string, object>>();
                ParamLogging.Add(new KeyValuePair<string, object>("@AppsettingId", listData.Id));
                string strSpNameLogging = "Get_Logging";
                var dataLogging = sqlHAsy.ExecuteAsObject<LogLevelInput>(strSpNameLogging, ParamLogging);

                List<KeyValuePair<string, object>> ParamLogLevel = new List<KeyValuePair<string, object>>();
                ParamLogLevel.Add(new KeyValuePair<string, object>("@AppsettingId", listData.Id));
                string strSpNameLogLevel = "Get_LogLevel";
                var dataLogLevel = sqlHAsy.ExecuteAsObject<LogLevelInput>(strSpNameLogLevel, ParamLogLevel);

                List<KeyValuePair<string, object>> ParamSettingToken = new List<KeyValuePair<string, object>>();
                ParamSettingToken.Add(new KeyValuePair<string, object>("@ApiName", input.AllowedScopes));
                string strSpNameSettingToken = "Get_SettingGateway";
                var dataSettingToken = sqlHAsy.ExecuteAsObject<GatewayInput>(strSpNameSettingToken, ParamSettingToken);

                result.Logging = new LoggingInput();

                result.ConnectionStrings = dataConnectionStrings;
                result.Logging.LogLevel = dataLogLevel;
                result.Setting = dataSettingToken;
                result.UrlPort = listData.UrlPort;
                result.AllowedHosts = listData.AllowedHosts;

                if (listData != null)
                {
                    outputJS.status = 1;
                    outputJS.result = result;

                    outputJS.code = "00";
                    outputJS.message = "Thanh cong";
                    outputJS.total = 1;
                    outputJS.page = 0;
                }
                else
                {
                    outputJS.status = 0;
                    outputJS.result = null;

                    outputJS.code = "01";
                    outputJS.message = "Khong tim thay du lieu";
                    outputJS.total = 0;
                    outputJS.page = 0;
                }

            }
            catch (Exception ex)
            {
                outputJS.status = 0;
                outputJS.result = null;

                outputJS.code = "01";
                outputJS.message = ex.Message;
                outputJS.total = 0;
                outputJS.page = 0;
            }

            return outputJS;
        }
    }
}
