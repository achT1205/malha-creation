using Cart.API.Configs;
using Cart.API.Services.ApiResponses;
using Cart.API.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Cart.API.Services;


public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private readonly string _productApiUrl;
    public ProductService(HttpClient httpClient, IOptions<ExternalApiSettings> externalApiSettings)
    {
        _httpClient = httpClient;
        _productApiUrl = externalApiSettings.Value.ProductApiUrl;
    }

    public async Task<Product?> GetProductByIdAsync(Guid productId)
    {
        var response = await _httpClient.GetAsync($"{_productApiUrl}/{productId}");

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();

            var resp = JsonSerializer.Deserialize<ProductApiResponse>(jsonString, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return resp?.Product;
        }

        throw new HttpRequestException("Failed to fetch stock data from external API.");
    }
}
