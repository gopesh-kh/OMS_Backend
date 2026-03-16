using Microsoft.AspNetCore.Mvc;
using OMS_Backend.DTOs.Product;
using OMS_Backend.Services;
using OMS_Backend.Utils;

namespace OMS_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] int? categoryId,
            [FromQuery] string? search,
            [FromQuery] string? sortBy,
            [FromQuery] bool isDescending,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int numberOfProductsPerPage = 10)
        {
            var result = await _service.GetProductsAsync(categoryId, search, sortBy, isDescending, pageNumber, numberOfProductsPerPage);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (Guard.IsInvalidId(id))
                return BadRequest("Provide a valid id");

            var result = await _service.GetByIdAsync(id);

            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto request)
        {
            if (Guard.IsNull(request))
                return BadRequest("Please provide valid product details.");

            var created = await _service.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.ProductId },
                created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto request)
        {
            if (Guard.IsInvalidId(id))
                return BadRequest("Please provide a valid id.");

            if (Guard.IsNull(request))
                return BadRequest("Please provide valid updated details.");

            var updated = await _service.UpdateAsync(id, request);

            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (Guard.IsInvalidId(id)) { return BadRequest("Please provide a valid id."); }

            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}