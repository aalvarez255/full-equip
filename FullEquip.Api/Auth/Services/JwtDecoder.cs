using FullEquip.Api.Auth.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FullEquip.Api.Auth.Services
{
    public class JwtDecoder : IJwtDecoder
    {
        public JwtSecurityToken Decode(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadToken(token) as JwtSecurityToken;
        }
    }
}
