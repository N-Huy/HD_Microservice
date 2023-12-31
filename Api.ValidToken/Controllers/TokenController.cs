using System.Net.Http;
using System.Threading.Tasks;
using Api.ValidToken.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Api.ValidToken.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ConfigurationModel _myConfiguration;
     //   private readonly ILogger _logger;
        public TokenController(IOptions<ConfigurationModel> myConfiguration)
        {
            _myConfiguration = myConfiguration.Value;
          //  _logger = logger;
        }
        [Route("authorize")]
        [HttpPost]
        public async Task<IActionResult> GenerateToken(inputParams input)
        {
          //  _logger.LogTrace(input.ClientSecret);
            var addressStr = _myConfiguration.Address;
            HttpClient httpClient = new HttpClient();
            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = addressStr,
                ClientId = input.ClientId,
                ClientSecret = input.ClientSecret,
                Scope = input.Scope 
                

            });
            return Ok(tokenResponse.Json);
        }

        [Route("authorizeLogin")]
        [HttpPost]
        public async Task<IActionResult> GenerateTokenLogin(inputParams input)
        {
            //  _logger.LogTrace(input.ClientSecret);
            var addressStr = _myConfiguration.Address;
            HttpClient httpClient = new HttpClient();
            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {

                Address = addressStr,
                ClientId = input.ClientId,
                ClientSecret = input.ClientSecret,
                Scope = input.Scope


            });
            return Ok(tokenResponse.Json);
        }
    }
}