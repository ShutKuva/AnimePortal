using Core.Abstractions.DTOs.Interfaces;

namespace Core.DTOs.Jwt
{
    public class JwtUserDto : JwtOnlyTokenDto, IUserDto
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}