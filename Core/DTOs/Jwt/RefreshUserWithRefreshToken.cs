namespace Core.DTOs.Jwt
{
    public class RefreshUserWithRefreshToken : RefreshUser
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}