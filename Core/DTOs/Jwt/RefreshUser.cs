namespace Core.DTOs.Jwt
{
    public class RefreshUser
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}