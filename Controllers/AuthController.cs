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
        public async Task<IActionResult> Register(CreateUserDto request)
        {
            if (Guard.IsNull(request)) return BadRequest("Please provide valid data.");

            var token = await _authService.RegisterAsync(request);

            if (Guard.IsNullOrWhiteSpace(token))
                return BadRequest("Unable to register user.");

            Response.Cookies.Append("jwt", token!, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(1)
            });

            return Ok(new
            {
                message = "User registered successfully"
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto request) {
            if (Guard.IsNull(request)) return BadRequest("Please provide valid data.");

            var token = await _authService.LoginAsync(request);

            if (Guard.IsNullOrWhiteSpace(token))
                return Unauthorized("Invalid email or password.");

            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(1)
            });

            return Ok(new
            {
                token = token!
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("jwt", new CookieOptions
            {
                HttpOnly = true,
                Secure = true
            });

            return Ok(new { message = "Logged out successfully" });
        }
    }
}
