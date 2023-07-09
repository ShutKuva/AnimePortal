using System.Security.Claims;
using AnimePortalAuthServer.Constants;
using Core.DB;
using Core.DTOs.Jwt;
using Core.DTOs.Others;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Interfaces;

namespace AnimePortalAuthServer.Controllers
{
    public class GoogleAuthController : BaseController
    {
        private readonly IJwtUserService<JwtUserDto, GoogleAuthUser, User, object> _googleAuthService;

        public GoogleAuthController(IJwtUserService<JwtUserDto, GoogleAuthUser, User, object> userService)
		{
            _googleAuthService = userService;
        }

        [HttpGet("google")]
        public IActionResult GoogleLogin(string returnUrl)

        {
            var redirectUri = $"{Url.Action("GoogleCallback")}?returnUrl={returnUrl}";
            var properties = new AuthenticationProperties { RedirectUri = redirectUri };
           
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google-callback")]
        public async Task<IActionResult> GoogleCallback(string returnUrl)
        {
            AuthenticateResult result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
           
            if (result == null || !result.Succeeded)
            {
                throw new ArgumentException("Google authentication error");
            }

            string name = result.Principal.Identity.Name;
            string email = result.Principal.Claims.FirstOrDefault(asd => asd.Type == ClaimTypes.Email).Value;

            var googleAuthUser = new GoogleAuthUser { Email = email, Name = name };

            JwtUserDto user = await _googleAuthService.RegisterNewUserAsync(googleAuthUser);

            ProcessUser(user);

            return Redirect(returnUrl);
        }

        private JwtOnlyTokenDto ProcessUser(JwtUserDto user)
        {
            Response.Cookies.Append(CookieConstants.REFRESH_CODE_COOKIE_NAME, user.RefreshToken, new CookieOptions()
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
            });

            return new JwtOnlyTokenDto
            {
                Token = user.Token
            };
        }
    }
}

