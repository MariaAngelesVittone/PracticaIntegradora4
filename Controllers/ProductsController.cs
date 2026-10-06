using Clase7BE.Models.DTOs.Requests;
using Clase7BE.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clase7BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);
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

        [HttpPost]
        public IActionResult CreateProduct(ProductForCreateDto dto)
        {
            if (_service.ProductNameExists(dto.Name))
            {
                return Conflict("Ya existe un producto con ese nombre.");
            }

            var created = _service.CreateProduct(dto);
            return CreatedAtAction(nameof(GetProductById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, ProductForUpdateDto dto)
        {
            if (_service.GetProductById(id) is null)
            {
                return NotFound();
            }

            _service.UpdateProduct(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            if (_service.GetProductById(id) is null)
            {
                return NotFound();
            }

            _service.DeleteProduct(id);
            return NoContent();
        }
    }
}
