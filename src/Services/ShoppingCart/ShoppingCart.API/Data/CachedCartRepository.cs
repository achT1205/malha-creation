using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Cart.API.Data;
public class CachedCartRepository(ICartRepository cartRepository, IDistributedCache cache)
    : ICartRepository
{
    public async Task<bool>  DeleteCart(Guid userId, CancellationToken cancellationToken = default)
    {
        await cartRepository.DeleteCart(userId, cancellationToken);
        await cache.RemoveAsync(userId.ToString(), cancellationToken);
        return true;
    }

    public async Task<Basket> GetCart(Guid userId, CancellationToken cancellationToken = default)
    {
        var cachedCart = await cache.GetStringAsync(userId.ToString(), cancellationToken);
        if (!string.IsNullOrEmpty(cachedCart))
            return JsonSerializer.Deserialize<Basket>(cachedCart)!;

        var cart = await cartRepository.GetCart(userId, cancellationToken);
        await cache.SetStringAsync(userId.ToString(), JsonSerializer.Serialize(cart));
        return cart;
    }

    public async Task<Basket> StoreCart(Basket cart, CancellationToken cancellationToken = default)
    {
        await cartRepository.StoreCart(cart, cancellationToken);
        await cache.SetStringAsync(cart.UserId.ToString(), JsonSerializer.Serialize(cart));

        return cart;
    }
}