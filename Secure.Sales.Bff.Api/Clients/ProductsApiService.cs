using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;
using Newtonsoft.Json;
using Secure.Sales.Bff.Api.Configs;
using Secure.Sales.Bff.Api.Dto.Responses;

namespace Secure.Sales.Bff.Api.Clients
{
    public class ProductsApiService : IProductsApiService
    {
        private readonly ProductsApiConfig _config;
        private readonly HttpClient _client;
        private readonly Uri _baseUri;

        public ProductsApiService(ProductsApiConfig config, HttpClient client)
        {
            _config = config;
            _client = client;
            _baseUri = new Uri(config.BaseUrl);
        }
        
        public async Task<AccessToken> GetAccessTokenAsync()
        {
            var resourceId = _config.ResourceId;

            var tokenCredential = new DefaultAzureCredential();
            var accessToken = await tokenCredential.GetTokenAsync(
                new TokenRequestContext(new[] { resourceId + "/.default" }) { }
            );
 
            return accessToken;
        }

        public async Task<GetProductResponse> GetProductByCodeAsync(string productCode)
        {
            var uri = new Uri(_baseUri, $"/products/{productCode}");
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, uri);
            
            var accessToken = await GetAccessTokenAsync();
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Token);
            
            var httpResponse = await _client.SendAsync(httpRequest);
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<GetProductResponse>(responseContent);

            return response;
        }
    }
}