using System.ComponentModel.DataAnnotations;

namespace FullEquip.Api.Auth.Dto
{
    public class TokenRequestDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
