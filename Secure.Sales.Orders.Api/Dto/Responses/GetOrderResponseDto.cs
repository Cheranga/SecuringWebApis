using System.Collections.Generic;
using Secure.Sales.Orders.Api.Dto.Assets;

namespace Secure.Sales.Orders.Api.Dto.Responses
{
    public class GetOrderResponseDto
    {
        public string OrderId { get; set; }
        public List<ProductDto> Products { get; set; }

        public GetOrderResponseDto()
        {
            Products = new List<ProductDto>();
        }
    }
}