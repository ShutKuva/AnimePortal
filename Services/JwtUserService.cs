﻿using BLL.Abstractions.Interfaces.Jwt;
using Core.ClaimNames;
using Core.DB;
using Core.DI;
using Core.DTOs.Jwt;
using DAL.Abstractions.Interfaces;
using Microsoft.Extensions.Options;
using Services.Abstraction.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BLL.Jwt
{
    public class JwtUserService : IUserService<JwtUserDto, RegisterUser, LoginUser, RefreshUserWithRefreshToken>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenHandler _tokenHandler;
        private readonly JwtConfigurations _jwtConfigurations;

        public JwtUserService(IUnitOfWork unitOfWork, IJwtTokenHandler tokenHandler, IOptions<JwtConfigurations> jwtConfigurations)
        {
            _unitOfWork = unitOfWork;
            _tokenHandler = tokenHandler;
            _jwtConfigurations = jwtConfigurations.Value ?? throw new ArgumentException(nameof(jwtConfigurations));
        }

        public async Task<JwtUserDto> RegisterNewUser(RegisterUser registerUser)
        {
            IEnumerable<User> usersWithSameName = await _unitOfWork.UserRepository.ReadByConditionAsync(user => user.Name == registerUser.Name);

            if (usersWithSameName.FirstOrDefault() != null)
            {
                throw new ArgumentException("User with this name has been already registered");
            }

            User newUser = new User
            {
                Name = registerUser.Name,
                PasswordHash = StringHasher.HashStringSHA256(registerUser.Password),
            };

            return await ProcessUser(newUser);
        }

        public async Task<JwtUserDto> LoginUser(LoginUser loginUser)
        {
            string hashedPassword = StringHasher.HashStringSHA256(loginUser.Password);

            IEnumerable<User> usersWithSameCredentials = await _unitOfWork.UserRepository.ReadByConditionAsync(user => user.Name == loginUser.Name && user.PasswordHash == hashedPassword);

            User? user = usersWithSameCredentials.FirstOrDefault();

            if (user == null)
            {
                throw new ArgumentException("Wrong credentials.");
            }

            return await ProcessUser(user);
        }

        public async Task<JwtUserDto> RefreshUser(RefreshUserWithRefreshToken refreshUser)
        {
            JwtSecurityToken token = new JwtSecurityToken(refreshUser.Token);

            bool successful = int.TryParse(token.Claims.FirstOrDefault(claim => claim.Type == UserClaimNames.Id)?.Value, out int id);

            if (!successful)
            {
                throw new ArgumentException("Unable to read user's id");
            }

            User? user = await _unitOfWork.UserRepository.ReadAsync(id);

            if (user == null)
            {
                throw new ArgumentException("There is no user with this id");
            }

            if (user.RefreshToken != refreshUser.RefreshToken)
            {
                throw new ArgumentException("Wrong refresh code.");
            }

            return await ProcessUser(user);
        }

        public async Task<JwtUserDto> ProcessUser(User user)
        {
            user.RefreshToken = _tokenHandler.CreateRefreshToken();
            user.RefreshTokenExpires = DateTime.Now.AddHours(_jwtConfigurations.RefreshLifetime).ToUniversalTime();

            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            JwtSecurityToken token = GenerateJwtTokenForUser(user);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return new JwtUserDto()
            {
                RefreshToken = user.RefreshToken,
                Token = handler.WriteToken(token)
            };
        }

        private JwtSecurityToken GenerateJwtTokenForUser(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(UserClaimNames.Name, user.Name),
                new Claim(UserClaimNames.Id, user.Id.ToString())
            };

            return _tokenHandler.CreateToken(claims);
        }
    }
}