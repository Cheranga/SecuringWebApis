using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Secure.Sales.Orders.Api.Dto.Assets;
using Secure.Sales.Orders.Api.Dto.Responses;

namespace Secure.Sales.Orders.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        [HttpGet("{orderId}")]
        [Authorize(Roles = "orders.search")]
        public IActionResult Get(string orderId)
        {
            var response = new GetOrderResponseDto
            {
                OrderId = orderId,
                Products = Enumerable.Range(1, 10).Select(x => new ProductDto
                {
                    Code = $"PROD{x}",
                    Name = $"Name {x}"
                }).ToList()
            };

            return Ok(response);
        }
    }
}