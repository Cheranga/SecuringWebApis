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
    public class OrdersApiService : IOrdersApiService
    {
        private readonly OrdersApiConfig _config;
        private readonly HttpClient _client;
        private readonly Uri _baseUri;

        public OrdersApiService(OrdersApiConfig config, HttpClient client)
        {
            _config = config;
            _client = client;
            _baseUri = new Uri(config.BaseUrl);
        }

        public async Task<AccessToken> GetAccessTokenAsync()
        {
            var ordersApiResourceId = _config.ResourceId;

            var tokenCredential = new ManagedIdentityCredential();
            var accessToken = await tokenCredential.GetTokenAsync(
                new TokenRequestContext(new[] { ordersApiResourceId + "/.default" }) { }
            );

            return accessToken;
        }

        public async Task<GetOrderResponseDto> GetOrderByIdAsync(string orderId)
        {
            var uri = new Uri(_baseUri, $"/orders/{orderId}");
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, uri);
            
            var accessToken = await GetAccessTokenAsync();
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Token);
            
            var httpResponse = await _client.SendAsync(httpRequest);
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<GetOrderResponseDto>(responseContent);

            return response;

        }
    }
}