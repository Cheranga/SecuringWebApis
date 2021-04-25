using System.Collections.Generic;
using Secure.Sales.Bff.Api.Dto.Assets;

namespace Secure.Sales.Bff.Api.Dto.Responses
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