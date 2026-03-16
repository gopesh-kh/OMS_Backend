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

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,         
                Secure = true,           
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(1)
            };

            Response.Cookies.Append("authToken", token!, cookieOptions);

            return Ok(new
            {
                message = "User registered successfully"
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

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("authToken", token!, cookieOptions);

            return Ok(new
            {
                message = "Login successful"
            });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("authToken");

            return Ok(new
            {
                message = "Logged out successfully"
            });
        }
    }
}