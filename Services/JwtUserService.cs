using BLL.Abstractions.Interfaces.Jwt;
using Core.ClaimNames;
using Core.DB;
using Core.DI;
using Core.DTOs.Jwt;
using Microsoft.Extensions.Options;
using Services.Abstraction.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BLL;

namespace Services
{
    public class JwtUserService : IJwtUserService<JwtUserDto, RegisterUser, LoginUser, RefreshUser>
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenHandler _tokenHandler;

        public JwtUserService(IUserService userService, IJwtTokenHandler tokenHandler, IOptions<JwtConfigurations> jwtConfigurations)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
        }

        public async Task<JwtUserDto> RegisterNewUserAsync(RegisterUser registerUser)
        {
            User? usersWithSameCredentials = await _userService.GetUserAsync(user => user.Name == registerUser.Name || user.Email == registerUser.Email);

            if (usersWithSameCredentials != null)
            {
                throw new ArgumentException("User with this credentials has been already registered");
            }

            User newUser = new User
            {
                Name = registerUser.Name,
                PasswordHash = registerUser.Password,
                Email = registerUser.Email,
            };

            return await ProcessUserAsync(newUser);
        }

        public async Task<JwtUserDto> LoginUserAsync(LoginUser loginUser)
        {
            User? user = await _userService.GetUserByCredentialsAsync(loginUser.NameOrEmail, loginUser.Password);

            if (user == null)
            {
                throw new ArgumentException("Wrong credentials.");
            }

            return await ProcessUserAsync(user);
        }

        public async Task<JwtUserDto> RefreshUserAsync(RefreshUser refreshUser)
        {
            ClaimsPrincipal cp = _tokenHandler.ValidateToken(refreshUser.RefreshToken, true);

            bool successful = int.TryParse(cp.Claims.FirstOrDefault(claim => claim.Type == UserClaimNames.Id)?.Value, out int id);

            if (!successful)
            {
                throw new ArgumentException("Unable to read user's id");
            }

            User? user = await _userService.GetUserAsync(u => u.Id == id);

            if (user == null)
            {
                throw new ArgumentException("There is no user with this id");
            }

            if (user.RefreshToken != refreshUser.RefreshToken)
            {
                throw new ArgumentException("Wrong refresh code.");
            }

            return await ProcessUserAsync(user);
        }

        public async Task<JwtUserDto> ProcessUserAsync(User user)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            user.RefreshToken = handler.WriteToken(GenerateJwtRefreshTokenForUser(user));

            await _userService.CreateUserAsync(user);

            JwtSecurityToken token = GenerateJwtAccessTokenForUser(user);

            return new JwtUserDto()
            {
                RefreshToken = user.RefreshToken,
                Token = handler.WriteToken(token)
            };
        }

        public async Task<bool> DoesNameOrEmailExist(string nameOrEmail)
        {
            User? user = await _userService.GetUserAsync(u => u.Name == nameOrEmail || u.Email == nameOrEmail);

            return user != null;
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
    }
}