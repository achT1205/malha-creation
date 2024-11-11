namespace Ordering.Infrastructure.Services;
public class CartService : ICartService
{
    private readonly HttpClient _httpClient;
    private readonly string _CartApiUrl;
    public CartService(HttpClient httpClient, IOptions<ExternalApiSettings> externalApiSettings)
    {
        _httpClient = httpClient;
        _CartApiUrl = externalApiSettings.Value.CartApiUrl;
    }

    public async Task<Basket?> GetCartByUserIdAsync(Guid userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_CartApiUrl}/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var resp = JsonSerializer.Deserialize<CartApiResponse>(jsonString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                return resp?.Cart;
            }

            throw new InternalServerException(response.Content.ToString());
        }
        catch (Exception ex)
        {

            throw new InternalServerException(ex.InnerException.Message);
        }
    }
}
