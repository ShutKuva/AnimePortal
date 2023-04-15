using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic.FileIO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Core.DI;

namespace BLL.Jwt
{
    public class JWTTokensManipulator
    {
        private readonly JwtConfigurations _jwtConfigurations;

        public JWTTokensManipulator(IOptions<JwtConfigurations> jwtConfigurations)
        {
            _jwtConfigurations = jwtConfigurations.Value ?? throw new ArgumentNullException("There are no jwt configurations on server");
        }

        public JwtSecurityToken CreateToken(List<Claim> claims)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfigurations.SecretCode));

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                issuer: _jwtConfigurations.Issuer,
                audience: _jwtConfigurations.Audience,
                expires: DateTime.Now.AddHours(_jwtConfigurations.Lifetime)
            );

            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] byteArray = new byte[64];
            Random rand = new Random();
            rand.NextBytes(byteArray);
            return Encoding.UTF8.GetString(byteArray);
        }
    }
}