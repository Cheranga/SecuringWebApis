using System.Threading.Tasks;
using Azure.Core;
using Secure.Sales.Bff.Api.Dto.Responses;

namespace Secure.Sales.Bff.Api.Clients
{
    public interface IOrdersApiService
    {
        Task<AccessToken> GetAccessTokenAsync();
        Task<GetOrderResponseDto> GetOrderByIdAsync(string orderId);
    }
}