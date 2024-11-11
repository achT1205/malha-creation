using BuildingBlocks.Exceptions;
using Catalog.Application.Services;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Catalog.Infrastructure.Services;

public class OrderingService : IOrderingService
{

    private readonly HttpClient _httpClient;
    private readonly string _orderApiUrl;
    public OrderingService(HttpClient httpClient, IOptions<ExternalApiSettings> externalApiSettings)
    {
        _httpClient = httpClient;
        _orderApiUrl = externalApiSettings.Value.OrderApiUrl;
    }

    public async Task<OrderStockDto?> GetOrderByIdAsync(Guid orderId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_orderApiUrl}/{orderId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var resp = JsonSerializer.Deserialize<OrderStockApiResponse>(jsonString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                return resp?.Order;
            }

            throw new InternalServerException(response.Content.ToString());
        }
        catch (Exception ex)
        {

            throw new InternalServerException(ex.InnerException.Message);
        }
    }

}
