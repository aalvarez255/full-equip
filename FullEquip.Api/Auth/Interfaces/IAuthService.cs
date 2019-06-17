using FullEquip.Api.Auth.Dto;
using System.Threading.Tasks;

namespace FullEquip.Api.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<TokenResponseDto> GetAccessTokenAsync(TokenRequestDto request);
        Task<TokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);
    }
}
