using Catalog.API.Configs;
using Catalog.API.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Catalog.API.Services;

public class SctockApiService : ISctockApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _stockApiUrl;
    public SctockApiService(HttpClient httpClient, IOptions<ExternalApiSettings> externalApiSettings)
    {
        _httpClient = httpClient;
        _stockApiUrl = externalApiSettings.Value.StockApiUrl;
    }

    public async Task<Stock?> GetStockByProductIdAsync(Guid productId)
    {
        var response = await _httpClient.GetAsync($"{_stockApiUrl}/{productId}");

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();

            var resp = JsonSerializer.Deserialize<StockApiResponse>(jsonString, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return resp?.Stock; 
        }

        throw new HttpRequestException("Failed to fetch stock data from external API.");
    }
}
