using Core.DI;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BLL.Abstractions.Interfaces.Jwt;

namespace BLL.Jwt
{
    public class JwtTokenHandler : IJwtTokenHandler
    {
        private readonly JwtConfigurations _jwtConfigurations;
        private const string ALL_CHARS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public JwtTokenHandler(IOptions<JwtConfigurations> jwtConfigurations)
        {
            _jwtConfigurations = jwtConfigurations.Value ?? throw new ArgumentNullException("There are no jwt configurations on server");
        }

        public JwtSecurityToken CreateToken(List<Claim> claims)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfigurations.SecretCode));

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
            for (int i = 0; i < byteArray.Length; i++)
            {
                byteArray[i] = (byte) ALL_CHARS[rand.Next(ALL_CHARS.Length)];
            }
            return Encoding.ASCII.GetString(byteArray);
        }
    }
}