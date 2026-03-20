using Microsoft.AspNetCore.Mvc;
using OMS_Backend.DTOs.Order;
using OMS_Backend.Services;
using OMS_Backend.Utils;

namespace OMS_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto request)
        {
            if (Guard.IsNull(request))
                return BadRequest("Invalid order request.");

            var created = await _service.CreateOrderAsync(request);

            if (created == null)
                return BadRequest("Order could not be created.");

            return CreatedAtAction(nameof(GetById), new { id = created.OrderId }, created);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserOrders(int userId)
        {
            if (Guard.IsInvalidId(userId))
                return BadRequest("Provide a valid user id.");

            var result = await _service.GetOrdersByUserAsync(userId);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (Guard.IsInvalidId(id))
                return BadRequest("Provide a valid id.");

            var result = await _service.GetOrderByIdAsync(id);

            return result == null ? NotFound() : Ok(result);
        }
    }
}