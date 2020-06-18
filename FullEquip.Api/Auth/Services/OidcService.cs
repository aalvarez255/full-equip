using FullEquip.Api.Auth.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FullEquip.Api.Auth.Services
{
    public class OidcService : IOidcService
    {
        private const string AzureAdUrl = "https://login.microsoftonline.com";
        private readonly IConfiguration _config;
        private readonly IJwtDecoder _jwtDecoder;

        public OidcService(
            IConfiguration config,
            IJwtDecoder jwtDecoder)
        {
            _config = config;
            _jwtDecoder = jwtDecoder;
        }

        public string GetAzureLoginUrl()
        {
            return $"{AzureAdUrl}/{_config["AzureAD:TenantId"]}/oauth2/authorize"
                    + $"?client_id={_config["AzureAD:ClientId"]}"
                    + $"&redirect_uri={_config["AzureAD:RedirectUri"]}"
                    + "&scope=openid%20profile%20email"
                    + "&response_type=code";
        }

        public async Task<string> GetAzureUserAsync(string code)
        {
            using var client = new HttpClient
            {
                BaseAddress = new Uri($"{AzureAdUrl}")
            };
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type","authorization_code"),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("client_id", _config["AzureAD:ClientId"]),
                new KeyValuePair<string, string>("client_secret", _config["AzureAD:ClientSecret"]),
                new KeyValuePair<string, string>("redirect_uri", _config["AzureAD:RedirectUri"]),
                new KeyValuePair<string, string>("scope", "openid%20profile%20email")
            });
            var result = await client.PostAsync($"/{ _config["AzureAD:TenantId"]}/oauth2/token", data);
            var content = await result.Content.ReadAsStringAsync();
            var json = JObject.Parse(content);
            var idToken = json.Value<string>("id_token");
            var jwt = _jwtDecoder.Decode(idToken);
            return jwt.Claims.First(x => x.Type == "email").Value;
        }
    }
}
