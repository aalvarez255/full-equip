using FullEquip.Api.Auth.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FullEquip.Api.Auth.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class OidcController : Controller
    {
        private readonly IOidcService _service;

        public OidcController(IOidcService service)
        {
            _service = service;
        }

        [HttpGet()]
        public IActionResult Oidc()
        {
            return Redirect(_service.GetAzureLoginUrl());
        }

        [HttpGet("token")]
        public async Task<IActionResult> Token(string code, string session_state)
        {
            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(session_state))
                return Unauthorized();

            var user = await _service.GetAzureUserAsync(code);
            return Ok();
        }
    }
}
