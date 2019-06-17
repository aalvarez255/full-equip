using FullEquip.Api.Auth.Dto;
using FullEquip.Infrastructure.DataAccess.Identity;
using System.Collections.Generic;

namespace FullEquip.Api.Auth.Interfaces
{
    public interface IJwtGenerator
    {
        AccessTokenDto GenerateJwtToken(User user, List<string> roles);
    }
}
