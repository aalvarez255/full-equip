using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FullEquip.Api.Auth.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class OidcController : Controller
    {
        private readonly IConfiguration _config;

        public OidcController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet()]
        public IActionResult Oidc()
        {
            return Redirect(
                $"https://login.microsoftonline.com/{_config["AzureAD:TenantId"]}/oauth2/authorize"
                    + $"?client_id={_config["AzureAD:ClientId"]}"
                    + $"&redirect_uri={_config["AzureAD:RedirectUri"]}"
                    + "&scope=openid%20profile%20email"
                    + "&response_type=code"
            );
        }

        [HttpGet("code")]
        public async Task<IActionResult> Code(string code, string session_state, string token)
        {
            if (String.IsNullOrEmpty(code) || String.IsNullOrEmpty(session_state))
                return Unauthorized();

            var s = await GetAzureAccessToken(code);
            return Ok();
        }

        [HttpGet("token")]
        public async Task<IActionResult> Token(string code, string token)
        {
            return Ok();
        }

        private async Task<string> GetAzureAccessToken(string code)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"https://login.microsoftonline.com");
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type","authorization_code"),
                    new KeyValuePair<string, string>("code", code),
                    new KeyValuePair<string, string>("client_id", _config["AzureAD:ClientId"]),
                    new KeyValuePair<string, string>("client_secret", _config["AzureAD:ClientSecret"]),
                    new KeyValuePair<string, string>("redirect_uri", _config["AzureAD:RedirectUri"])
                });
                var result = await client.PostAsync($"/{ _config["AzureAD:TenantId"]}/oauth2/token", content);
                var json = JObject.LoadAsync(await result.Content.ReadAsStringAsync());
                return json
            }
        }
    }
}
