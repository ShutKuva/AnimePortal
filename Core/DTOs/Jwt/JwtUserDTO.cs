using Core.Abstractions.DTOs.Interfaces;

namespace Core.DTOs.Jwt
{
    public class JwtUserDTO : IUserDTO
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}