using FullEquip.Api.Auth.Dto;
using FullEquip.Api.Auth.Exceptions;
using FullEquip.Api.Auth.Interfaces;
using FullEquip.Infrastructure.DataAccess.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace FullEquip.Api.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IJwtValidator _jwtValidator;

        public AuthService(
            UserManager<User> userManager,
            IJwtGenerator jwtGenerator,
            IJwtValidator jwtValidator)
        {
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
            _jwtValidator = jwtValidator;
        }

        public async Task<TokenResponseDto> GetAccessTokenAsync(TokenRequestDto request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                throw new AuthException(AuthErrors.InvalidCredentials);

            if (!await _userManager.CheckPasswordAsync(user, request.Password))
                throw new AuthException(AuthErrors.InvalidCredentials);

            // generate refresh token
            var refreshToken = GenerateRefreshToken();
            user.AddRefreshToken(refreshToken);
            await _userManager.UpdateAsync(user);

            var roles = await _userManager.GetRolesAsync(user);
            return new TokenResponseDto()
            {
                AccessToken = _jwtGenerator.GenerateJwtToken(user, roles.ToList()),
                RefreshToken = refreshToken
            };
        }

        public async Task<TokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request)
        {
            var cp = _jwtValidator.GetPrincipalFromToken(request.AccessToken);

            // invalid token/signing key was passed and we can't extract user claims
            if (cp == null)
                throw new AuthException(AuthErrors.InvalidToken);

            var id = Guid.Parse(cp.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var user = _userManager.Users.Include(x => x.RefreshTokens).SingleOrDefault(x => x.Id == id);

            if (user == null)
                throw new AuthException(AuthErrors.InvalidToken);

            if (!user.HasValidRefreshToken(request.RefreshToken))
                throw new AuthException(AuthErrors.InvalidToken);
            
            var refreshToken = GenerateRefreshToken();
            user.RemoveRefreshToken(request.RefreshToken); // delete the token we've exchanged
            user.AddRefreshToken(refreshToken); // add the new one
            await _userManager.UpdateAsync(user);

            var roles = await _userManager.GetRolesAsync(user);
            return new TokenResponseDto()
            {
                AccessToken = _jwtGenerator.GenerateJwtToken(user, roles.ToList()),
                RefreshToken = refreshToken
            };
        }

        private string GenerateRefreshToken()
        {
            int size = 32;
            var randomNumber = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
