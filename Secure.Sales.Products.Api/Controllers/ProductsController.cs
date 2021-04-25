using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Secure.Sales.Products.Api.Dto.Responses;

namespace Secure.Sales.Products.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet("{productId}")]
        [Authorize(Roles = "products.search")]
        public IActionResult Get([FromRoute] string productId)
        {
            var response = new GetProductResponse
            {
                Code = productId,
                Name = $"Product {productId}"
            };

            return Ok(response);
        }
        
    }
}