using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BLL.Abstractions.Interfaces.Jwt
{
    public interface IJwtTokenHandler
    {
        JwtSecurityToken CreateToken(List<Claim> claims);
        public string CreateRefreshToken();
    }
}