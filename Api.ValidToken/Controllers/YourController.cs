using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Api.ValidToken.Models;
using Api.ValidToken.Log;
using System;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Newtonsoft.Json;
using SQLHelper;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Razor.Language;
using System.Linq;

namespace Api.ValidToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YourController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public YourController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


       

        [Route("radi")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetDataFromDatabase()
        {

            string connectionString = "Server=10.0.65.120;Database=DB_Microservice;user id=sa;password=@lo123456789!@#;";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("SELECT * FROM ReRoutes", connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<string> data = new List<string>();

                            while (await reader.ReadAsync())
                            {
                                // Thay YourColumnName bằng tên cột chứa dữ liệu bạn muốn lấy
                                string value = reader["UpstreamPathTemplate"].ToString();
                                data.Add(value);
                            }

                            return Ok(data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần thiết
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [Route("conbo")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiResource>>> GetAllApiResources1()
        {

            string connectionString = Startup.ConnectionConfig.GetSection("DefaultConnection").Value;
            List<ApiResource> data = new List<ApiResource>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Resource", connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            var newResource = new ApiResource();
                            newResource.Name = reader["Name"].ToString();
                            newResource.DisplayName = reader["DisplayName"].ToString();

                            var scope = new Scope();
                            scope.Name = newResource.Name;
                            scope.DisplayName = newResource.DisplayName;

                            newResource.Scopes.Add(scope);

                            data.Add(newResource);
                        }
                    }
                }
            }

            return data;

            //return new List<ApiResource>
            //{
            //    new ApiResource("QLHCSCOPE","HDBANK MOBILE"),
            //    new ApiResource("QLHC.PTVC","HDBANK SCOPE") ,
            //    new ApiResource("HDBANKSCOPE","HDBANK SCOPE"),
            //    new ApiResource("TESTSCOPE","HDBANK SCOPE")
            //};
        }


        public static async Task<List<ApiResource>> GetResource()
        {
            OutputJS outputJS = new OutputJS();

            var result = new List<ApiResource>();
            try
            {

                LogWriter log = new LogWriter("StaticController ==> getresource ==> " + DateTime.Now.ToString() + " ==> " + "getresource");

                string strSpName = "Get_Resource";
                SQLHandler sqlHAsy = new SQLHandler();
                var listData = sqlHAsy.ExecuteAsList<ApiResource>(strSpName);

                foreach (var item in listData)
                {
                    var scope = new Scope();
                    item.Scopes.Add(scope);
                }

                result = listData;
                string JSONString = string.Empty;

                if (listData.Count > 0)
                {
                    outputJS.status = 1;
                    outputJS.result = listData;

                    outputJS.code = "00";
                    outputJS.message = "Thanh cong";
                    outputJS.total = listData.Count;
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

            return result;
        }

       


        [HttpPost("getstatics")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<OutputJS> GetStatics(testInput input)
        {
            OutputJS outputJS = new OutputJS();
            try
            {
                List<KeyValuePair<string, object>> Param = new List<KeyValuePair<string, object>>();
                Param.Add(new KeyValuePair<string, object>("@ReRoutesId", input.ReRoutesId));

                var jsonParams = JsonConvert.SerializeObject(input);
                LogWriter log = new LogWriter("StaticController ==> getstatics ==> " + DateTime.Now.ToString() + " ==> " + jsonParams);

                string strSpName = "Get_FileCacheOptions";
                SQLHandler sqlHAsy = new SQLHandler();
                var listData = sqlHAsy.ExecuteAsList<FileCacheOptions>(strSpName, Param);
                string JSONString = string.Empty;

                if (listData.Count > 0)
                {
                    outputJS.status = 1;
                    outputJS.result = listData;

                    outputJS.code = "00";
                    outputJS.message = "Thanh cong";
                    outputJS.total = listData.Count;
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

                string jsonData = JsonConvert.SerializeObject(listData);

                // Ghi dữ liệu vào configuration.json
                var templatePath = $"{Environment.CurrentDirectory}/testjson.json";
                string configFilePath = "testjson.json";
                System.IO.File.WriteAllText(templatePath, jsonData);
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
                LogWriter log = new LogWriter("MicroserviceController ==> GetReRoutes ==> " + DateTime.Now.ToString() + " ==> ");

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
                var templatePath = $"{Environment.CurrentDirectory}\\Controllers\\testjson.json";
                string configFilePath = "testjson.json";
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
    }
}
