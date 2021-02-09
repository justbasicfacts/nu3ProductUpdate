using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nu3ProductUpdate.Data.Interfaces;
using nu3ProductUpdate.Models;
using System.Collections.Generic;

namespace nu3ProductUpdate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize()]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _productsService.FindAll();
        }

        [HttpGet("{handle}", Name = "GetByHandle")]
        public ActionResult<Product> Get(string handle)
        {
            var result = _productsService.GetByHandle(handle);
            if (result != default)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult Insert(Product product)
        {
            var id = _productsService.Insert(product);
            if (id != default)
                return CreatedAtRoute("GetByHandle", new { id }, product);
            else
                return BadRequest();
        }

        [HttpPut("{handle}", Name = "UpdateByHandle")]
        public IActionResult Update(Product product)
        {
            var result = _productsService.Update(product);
            if (result)
                return NoContent();
            else
                return NotFound();
        }
    }
}