using System.Threading.Tasks;
using Azure.Core;
using Secure.Sales.Bff.Api.Dto.Responses;

namespace Secure.Sales.Bff.Api.Clients
{
    public interface IProductsApiService
    {
        Task<AccessToken> GetAccessTokenAsync();
        Task<GetProductResponse> GetProductByCodeAsync(string productCode);
    }
}