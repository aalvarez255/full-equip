using System.Security.Claims;

namespace FullEquip.Api.Auth.Interfaces
{
    public interface IJwtValidator
    {
        ClaimsPrincipal GetPrincipalFromToken(string token);
    }
}
