using AnimePortalAuthServer.Constants;
using AnimePortalAuthServer.Controllers;
using Core.Abstractions.DTOs.Interfaces;
using Core.DTOs.Jwt;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Interfaces;

namespace AnimePortal.Controllers
{
    public class JwtAuthController : BaseController
    {
        private readonly IUserService<JwtUserDto, RegisterUser, LoginUser, RefreshUserWithRefreshToken> _userService;

        public JwtAuthController(IUserService<JwtUserDto, RegisterUser, LoginUser, RefreshUserWithRefreshToken> userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<CreatedResult> RegisterUser([FromBody] RegisterUser userModel)
        {
            JwtUserDto user = await _userService.RegisterNewUser(userModel);

            JwtOnlyTokenDto result = ProcessUser(user);

            return Created(string.Empty, result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<IUserDto>> LoginUser([FromBody] LoginUser userModel)
        {
            JwtUserDto user = await _userService.LoginUser(userModel);

            JwtOnlyTokenDto result = ProcessUser(user);

            return Ok(result);
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<IUserDto>> RefreshJwtToken([FromBody] RefreshUser refreshUser)
        {
            string? refreshToken = Request.Cookies[CookieConstants.REFRESH_CODE_COOKIE_NAME];

            if (refreshToken == null)
            {
                throw new ArgumentException("There is no refresh token.");
            }

            RefreshUserWithRefreshToken refreshUserWithRefreshToken = new RefreshUserWithRefreshToken()
            {
                Token = refreshUser.Token,
                RefreshToken = refreshToken
            };

            JwtUserDto user = await _userService.RefreshUser(refreshUserWithRefreshToken);

            JwtOnlyTokenDto result = ProcessUser(user);

            return Ok(result);
        }

        private JwtOnlyTokenDto ProcessUser(JwtUserDto user)
        {
            Response.Cookies.Append(CookieConstants.REFRESH_CODE_COOKIE_NAME, user.RefreshToken, new CookieOptions()
            {
                HttpOnly = true,
            });

            return new JwtOnlyTokenDto
            {
                Token = user.Token
            };
        }
    }
}
