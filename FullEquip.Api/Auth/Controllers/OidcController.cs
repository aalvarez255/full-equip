using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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

        [HttpGet("login")]
        public IActionResult Login(string code, string session_state)
        {
            return Ok();
        }
    }
}
