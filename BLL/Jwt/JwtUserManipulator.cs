using BLL.Abstractions.Interfaces;
using Core.Abstractions.DTOs.Interfaces;
using Core.ClaimNames;
using Core.DB;
using Core.DI;
using Core.DTOs.Jwt;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BLL.Jwt
{
    public class JwtUserManipulator : IUserManipulator<User>
    {
        private ICrudService<User> _userCrudService;
        private readonly JwtGeneralHelper _jwtGeneralHelper;

        public JwtUserManipulator(ICrudService<User> userCrudService, JwtGeneralHelper jwtGeneralHelper)
        {
            _userCrudService = userCrudService;
            _jwtGeneralHelper = jwtGeneralHelper;
        }

        public async Task<IUserDTO> RegisterNewUser(RegisterUser userModel)
        {
            IEnumerable<User> usersWithSameName = await _userCrudService.ReadByCondition(user => user.Name == userModel.Name);

            if (usersWithSameName.FirstOrDefault() != null)
            {
                throw new ArgumentException("User with this name has been already registered");
            }

            User newUser = new User
            {
                Name = userModel.Name,
                PasswordHash = StringHasher.HashStringSHA256(userModel.Password)
            };

            return await _jwtGeneralHelper.ProcessUser(newUser);
        }

        public async Task<IUserDTO> LoginUser(LoginUser loginUser)
        {
            IEnumerable<User> usersWithSameCredentials = await _userCrudService.ReadByCondition(user => user.Name == loginUser.Name && user.PasswordHash == StringHasher.HashStringSHA256(loginUser.Password));

            User user = usersWithSameCredentials.FirstOrDefault();

            if (user == null)
            {
                throw new ArgumentException("User with this name has been already registered");
            }

            return await _jwtGeneralHelper.ProcessUser(user);
        }
    }
}