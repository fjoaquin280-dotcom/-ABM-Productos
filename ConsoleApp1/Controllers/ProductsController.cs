using ConsoleApp1.Models.DTOs.Requests;
using ConsoleApp1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConsoleApp1.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(_service.GetAllProducts());
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _service.GetProductById(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductForCreateDto dto)
        {
            var product = _service.CreateProduct(dto);

            if (product == null)
            {
                return Conflict("Ya existe un producto con ese nombre.");
            }

            return CreatedAtAction(
                nameof(GetProductById),
                new { id = product.Id },
                product);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, ProductForUpdateDto dto)
        {
            var product = _service.GetProductById(id);

            if (product == null)
                return NotFound();

            _service.UpdateProduct(id, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _service.GetProductById(id);

            if (product == null)
                return NotFound();

            _service.DeleteProduct(id);

            return NoContent();
        }

        [HttpGet("search")]
        public IActionResult SearchProductsByName(string name)
        {
            return Ok(_service.SearchProductsByName(name));
        }

        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            return Ok(_service.GetStats());
        }
    }
}