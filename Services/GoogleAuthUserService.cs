using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BLL.Abstractions.Interfaces.Jwt;
using Core.ClaimNames;
using Core.DB;
using Core.DTOs.Jwt;
using Core.DTOs.Others;
using DAL.Abstractions.Interfaces;
using Services.Abstraction.Interfaces;

namespace Services
{
    public class GoogleAuthUserService : IJwtUserService<JwtUserDto, GoogleAuthUser, User, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenHandler _tokenHandler;

        public GoogleAuthUserService(IUnitOfWork unitOfWork, IJwtTokenHandler tokenHandler)
		{
            _unitOfWork = unitOfWork;
            _tokenHandler = tokenHandler;
        }

        public async Task<JwtUserDto> LoginUserAsync(User user)
        {
            return await ProcessUserAsync(user);
        }

        public async Task<JwtUserDto> RegisterNewUserAsync(GoogleAuthUser registerUser)
        {
            IEnumerable<User> usersWithSameCredentials = await _unitOfWork.UserRepository.ReadByConditionAsync(user => user.Name == registerUser.Name || user.Email == registerUser.Email);
            User user = usersWithSameCredentials.FirstOrDefault();

            if (user != null)
            {
                return await LoginUserAsync(user);
            }

            User newUser = new User
            {
                Name = registerUser.Name,
                Email = registerUser.Email,
            };

            return await ProcessUserAsync(newUser);
        }

        public async Task<JwtUserDto> ProcessUserAsync(User user)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            user.RefreshToken = handler.WriteToken(GenerateJwtRefreshTokenForUser(user));

            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            JwtSecurityToken token = GenerateJwtAccessTokenForUser(user);

            return new JwtUserDto()
            {
                RefreshToken = user.RefreshToken,
                Token = handler.WriteToken(token)
            };
        }

        private JwtSecurityToken GenerateJwtAccessTokenForUser(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(UserClaimNames.Name, user.Name),
                new Claim(UserClaimNames.Id, user.Id.ToString())
            };

            return _tokenHandler.CreateAccessToken(claims);
        }

        private JwtSecurityToken GenerateJwtRefreshTokenForUser(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(UserClaimNames.Id, user.Id.ToString())
            };

            return _tokenHandler.CreateRefreshToken(claims);
        }

        public Task<JwtUserDto> RefreshUserAsync(object refreshUser)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DoesNameOrEmailExist(string nameOrEmail)
        {
            throw new NotImplementedException();
        }
    }
}

