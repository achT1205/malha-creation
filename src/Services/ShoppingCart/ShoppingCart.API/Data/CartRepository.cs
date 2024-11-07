using Cart.API.Exceptions;

namespace Cart.API.Data;
public class CartRepository(IDocumentSession session) : ICartRepository
{
    public async Task<bool> DeleteCart(Guid userId, CancellationToken cancellationToken = default)
    {
        var cart = await session.LoadAsync<Basket>(userId, cancellationToken);
        if (cart == null)
        {
            throw new CartNotFoundException(userId.ToString());
        }
        session.Delete<Basket>(userId);
        await session.SaveChangesAsync();
        return true;
    }

    public async Task<Basket> GetCart(Guid userId, CancellationToken cancellationToken = default)
    {
        var cart = await session.LoadAsync<Basket>(userId, cancellationToken);
        return cart is null ? throw new CartNotFoundException(userId.ToString()) : cart;
    }

    public async Task<Basket> StoreCart(Basket cart, CancellationToken cancellationToken = default)
    {
        session.Store(cart);
        await session.SaveChangesAsync(cancellationToken);
        return cart;
    }
}