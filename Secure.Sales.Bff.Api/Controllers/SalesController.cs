using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Secure.Sales.Bff.Api.Clients;

namespace Secure.Sales.Bff.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly IOrdersApiService _ordersApiService;
        private readonly IProductsApiService _productsApiService;

        public SalesController(IOrdersApiService ordersApiService, IProductsApiService productsApiService)
        {
            _ordersApiService = ordersApiService;
            _productsApiService = productsApiService;
        }
        
        [HttpGet("orders/{orderId}")]
        [Authorize(Roles = "sales.bff.orders.search")]
        public async Task<IActionResult> Get([FromRoute] string orderId)
        {
            var ordersToken = await _ordersApiService.GetAccessTokenAsync();
            var order = await _ordersApiService.GetOrderByIdAsync(orderId);

            var productsToken = await _productsApiService.GetAccessTokenAsync();
            var product = await _productsApiService.GetProductByCodeAsync("prod1");

            var response = new
            {
                ordersToken,
                productsToken,
                order,
                product
            };

            return Ok(response);
        }
    }
}