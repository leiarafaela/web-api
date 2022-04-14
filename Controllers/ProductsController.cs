using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsService _productsService;

        public ProductsController(ProductsService productsService) =>
            _productsService = productsService;

        [HttpGet]
        public async Task<List<Product>> Get() =>
            await _productsService.GetAsync();


        [HttpPost]
        public async Task<IActionResult> Post(Product newProduct)
        {
            await _productsService.CreateAsync(newProduct);

            return CreatedAtAction(nameof(Get), new { id = newProduct.Id}, newProduct);

        }
            
        
        
    }
}