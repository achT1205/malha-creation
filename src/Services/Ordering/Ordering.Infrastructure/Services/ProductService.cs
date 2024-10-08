using Newtonsoft.Json;

namespace Ordering.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<object?> GetProduct(string productId)
    {
        var productResponse = await _httpClient.GetStringAsync($"https://product-service/api/products/{productId}");

        // Désérialisation de la réponse JSON du service REST
        var product = JsonConvert.DeserializeObject<object>(productResponse);

        return product;
    }
}
