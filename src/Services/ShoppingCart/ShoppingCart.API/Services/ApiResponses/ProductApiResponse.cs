using System.Text.Json.Serialization;

namespace Cart.API.Services.ApiResponses;

public class ProductApiResponse
{
    [JsonPropertyName("product")]
    public Product Product { get; set; } = new();
}