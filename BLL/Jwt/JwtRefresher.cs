using BLL.Abstractions.Interfaces;
using Core.ClaimNames;
using Core.DB;
using Core.DTOs.Jwt;
using System.IdentityModel.Tokens.Jwt;

namespace BLL.Jwt
{
    public class JwtRefresher
    {
        private readonly ICrudService<User> _userCrudService;
        private readonly JwtGeneralHelper _jwtGeneralHelper;

        public JwtRefresher(ICrudService<User> userCrudService, JwtGeneralHelper jwtGeneralHelper)
        {
            _userCrudService = userCrudService;
            _jwtGeneralHelper = jwtGeneralHelper;
        }

        public async Task<JwtUserDTO> RefreshToken(RefreshUser refreshUser)
        {
            JwtSecurityToken token = new JwtSecurityToken(refreshUser.Token);

            bool successful = int.TryParse(token.Claims.FirstOrDefault(claim => claim.Type == UserClaimNames.Id)?.Value, out int id);

            if (!successful)
            {
                throw new ArgumentException("Unable to read user's id");
            }

            User? user = await _userCrudService.Read(id);

            if (user == null)
            {
                throw new ArgumentException("There is no user with this id");
            }

            return await _jwtGeneralHelper.ProcessUser(user);
        }
    }
}