using Microsoft.AspNetCore.Mvc;
using API.Services;
using API.Models;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] ProductQueryParameters parameters)
        {
            var products = await _productService.GetProducts(parameters);
            return Ok(products);
        }
        
        [HttpGet("{id}/related")]
        public async Task<IActionResult> GetRelatedProducts(int id)
        {
            var relatedProducts = await _productService.GetRelatedProducts(id);
            return Ok(relatedProducts);
        }
    }
}