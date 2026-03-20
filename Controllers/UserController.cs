using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS_Backend.DTOs.User;
using OMS_Backend.Services;
using OMS_Backend.Utils;

namespace OMS_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] QueryParams query)
        {
            var (data, total) = await _service.GetUsersAsync(query);

            return Ok(new
            {
                data,
                total,
                page = query.PageNumber,
                pageSize = query.PageSize
            });
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (Guard.IsInvalidId(id))
                return BadRequest("Provide a valid id");

            var result = await _service.GetByIdAsync(id);

            if (Guard.IsNull(result)) { return BadRequest("Could not find user, provide valid request"); }

            return result == null ? NotFound() : Ok(result);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto request)
        {
            if (Guard.IsInvalidId(id))
                return BadRequest("Provide a valid id");

            if (!ModelState.IsValid || Guard.IsNull(request))
                return BadRequest("Invalid update request");

            var updated = await _service.UpdateAsync(id, request);

            return updated ? NoContent() : NotFound();
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);

            var result = await _service.GetByIdAsync(userId);

            return result == null ? NotFound() : Ok(result);
        }

        [Authorize]
        [HttpPut("me")]
        public async Task<IActionResult> UpdateCurrentUser([FromBody] UpdateUserDto request)
        {
            if (!ModelState.IsValid || Guard.IsNull(request))
                return BadRequest("Invalid update request");

            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);

            var updated = await _service.UpdateAsync(userId, request);

            return updated ? NoContent() : NotFound();
        }
    }
}