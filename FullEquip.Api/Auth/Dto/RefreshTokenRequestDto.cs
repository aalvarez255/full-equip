using System.ComponentModel.DataAnnotations;

namespace FullEquip.Api.Auth.Dto
{
    public class RefreshTokenRequestDto
    {
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
