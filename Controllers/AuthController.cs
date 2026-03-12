using Microsoft.AspNetCore.Mvc;
using OMS_Backend.Services;
using OMS_Backend.Utils;

namespace OMS_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto request)
        {
            if (Guard.IsNull(request))
                return BadRequest("Request body cannot be empty.");

            var token = await _authService.RegisterAsync(request);

            if (Guard.IsNullOrEmpty(token))
                return BadRequest("Unable to register user.");

            return Ok(new
            {
                token
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto request)
        {
            if (Guard.IsNull(request))
                return BadRequest("Request body cannot be empty.");

            var token = await _authService.LoginAsync(request);

            if (Guard.IsNullOrEmpty(token))
                return Unauthorized("Invalid email or password.");

            return Ok(new
            {
                token
            });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok(new
            {
                message = "Logged out successfully"
            });
        }
    }
}