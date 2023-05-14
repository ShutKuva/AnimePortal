using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AnimePortalAuthServer.Controllers
{
	public class GoogleAuthController : BaseController
    {
		public GoogleAuthController()
		{
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

            if (result?.Succeeded == true)
            {

                return Redirect(returnUrl);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

