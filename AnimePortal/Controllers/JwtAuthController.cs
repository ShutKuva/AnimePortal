using AnimePortalAuthServer.Controllers;
using BLL.Abstractions.Interfaces;
using BLL.Jwt;
using Core.Abstractions.DTOs.Interfaces;
using Core.DB;
using Core.DTOs.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace AnimePortal.Controllers
{
    public class JwtAuthController : BaseController
    {
        private readonly IUserManipulator<User> _userManipulator;
        private readonly JwtRefresher _jwtRefresher;

        public JwtAuthController(IUserManipulator<User> userManipulator, JwtRefresher jwtRefresher)
        {
            _userManipulator = userManipulator;
            _jwtRefresher = jwtRefresher;
        }

        [HttpPost("register")]
        public async Task<CreatedResult> RegisterUser([FromBody] RegisterUser userModel)
        {
            IUserDTO user = await _userManipulator.RegisterNewUser(userModel);

            return Created(string.Empty, user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<IUserDTO>> LoginUser([FromBody] LoginUser userModel)
        {
            IUserDTO user = await _userManipulator.LoginUser(userModel);

            return Ok(user);
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<IUserDTO>> RefreshJwtToken([FromBody] RefreshUser refreshUser)
        {
            IUserDTO user = await _jwtRefresher.RefreshToken(refreshUser);

            return Ok(user);
        }
    }
}
