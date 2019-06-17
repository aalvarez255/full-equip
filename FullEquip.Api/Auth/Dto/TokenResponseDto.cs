namespace FullEquip.Api.Auth.Dto
{
    public class TokenResponseDto
    {
        public AccessTokenDto AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class AccessTokenDto
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
