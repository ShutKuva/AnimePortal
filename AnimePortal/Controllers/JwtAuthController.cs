using BLL.Abstractions;
using BLL.Jwt;
using Core;
using Core.Abstractions.DTOs.Interfaces;
using Core.DB;
using Core.DTOs.Jwt;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtAuthController : ControllerBase
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

        [HttpGet("login")]
        public async Task<OkObjectResult> LoginUser([FromBody] LoginUser userModel)
        {
            IUserDTO user = await _userManipulator.LoginUser(userModel);

            return Ok(user);
        }

        public async Task<OkObjectResult> RefreshJwtToken([FromBody] RefreshUser refreshUser)
        {
            IUserDTO user = await _jwtRefresher.RefreshToken(refreshUser);

            return Ok(user);
        }
    }
}
