using System.IdentityModel.Tokens.Jwt;

namespace FullEquip.Api.Auth.Interfaces
{
    public interface IJwtDecoder
    {
        JwtSecurityToken Decode(string token);
    }
}
