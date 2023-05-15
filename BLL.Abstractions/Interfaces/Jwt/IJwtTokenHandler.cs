using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BLL.Abstractions.Interfaces.Jwt
{
    public interface IJwtTokenHandler
    {
        JwtSecurityToken CreateAccessToken(List<Claim> claims);
        JwtSecurityToken CreateRefreshToken(List<Claim> claims);
        ClaimsPrincipal ValidateToken(string token);
    }
}