using BLL.Abstractions;
using Core.ClaimNames;
using Core.DB;
using Core.DI;
using Core.DTOs.Jwt;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BLL.Jwt
{
    public class JwtGeneralHelper
    {
        private readonly ICrudService<User> _userCrudService;
        private readonly JWTTokensManipulator _jWTTokensManipulator;
        private readonly JwtConfigurations _jwtConfigurations;

        public JwtGeneralHelper(
            ICrudService<User> userCrudService, 
            JWTTokensManipulator jWTTokensManipulator,
            IOptions<JwtConfigurations> jwtConfigurations)
        {
            _userCrudService = userCrudService;
            _jWTTokensManipulator = jWTTokensManipulator;
            _jwtConfigurations = jwtConfigurations.Value ?? throw new ArgumentNullException(nameof(jwtConfigurations));
        }

        public async Task<JwtUserDTO> ProcessUser(User user)
        {
            user.RefreshToken = _jWTTokensManipulator.CreateRefreshToken();
            user.RefreshTokenExpires = DateTime.Now.AddHours(_jwtConfigurations.RefreshLifetime).ToUniversalTime();

            await _userCrudService.Update(user);

            JwtSecurityToken token = GenerateJwtTokenForUser(user);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return new JwtUserDTO
            {
                Token = handler.WriteToken(token),
                RefreshToken = user.RefreshToken
            };
        }

        private JwtSecurityToken GenerateJwtTokenForUser(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(UserClaimNames.Name, user.Name),
                new Claim(UserClaimNames.Id, user.Id.ToString())
            };

            return _jWTTokensManipulator.CreateToken(claims);
        }
    }
}