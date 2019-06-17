using FullEquip.Api.Auth.Dto;
using FullEquip.Api.Auth.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FullEquip.Api.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST api/auth/login
        [HttpPost("token")]
        public async Task<ActionResult> Token([FromBody]TokenRequestDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(
                await _authService.GetAccessTokenAsync(request)
            );
        }

        // POST api/auth/refreshtoken
        [HttpPost("refresh")]
        public async Task<ActionResult> RefreshToken([FromBody]RefreshTokenRequestDto request)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            return Ok(
               await _authService.RefreshTokenAsync(request)
           );
        }
    }
}
