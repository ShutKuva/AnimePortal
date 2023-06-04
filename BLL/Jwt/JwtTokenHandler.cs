using Core.DI;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BLL.Abstractions.Interfaces.Jwt;
using Azure.Core;

namespace BLL.Jwt
{
    public class JwtTokenHandler : IJwtTokenHandler
    {
        private readonly JwtConfigurations _jwtConfigurations;

        public JwtTokenHandler(IOptions<JwtConfigurations> jwtConfigurations)
        {
            _jwtConfigurations = jwtConfigurations.Value ?? throw new ArgumentNullException("There are no jwt configurations on server");
        }

        public JwtSecurityToken CreateAccessToken(List<Claim> claims)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfigurations.AccessSecretCode));

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                issuer: _jwtConfigurations.Issuer,
                audience: _jwtConfigurations.Audience,
                expires: DateTime.Now.AddHours(_jwtConfigurations.AccessLifetime)
            );

            return token;
        }

        public JwtSecurityToken CreateRefreshToken(List<Claim> claims)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfigurations.RefreshSecretCode));

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                issuer: _jwtConfigurations.Issuer,
                audience: _jwtConfigurations.Audience,
                expires: DateTime.Now.AddHours(_jwtConfigurations.RefreshLifetime)
            );

            return token;
        }

        public ClaimsPrincipal ValidateToken(string strToken, bool isRefresh)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(isRefresh ? _jwtConfigurations.RefreshSecretCode : _jwtConfigurations.AccessSecretCode)),
                ValidateAudience = false,
                ValidateIssuer = false,
            };

            SecurityToken token;

            ClaimsPrincipal cp = handler.ValidateToken(strToken, validationParameters, out token);

            return cp;
        }
    }
}