using Cart.API.Exceptions;

namespace Cart.API.Data;
public class CartRepository(IDocumentSession session) : ICartRepository
{
    public async Task<bool> DeleteCart(Guid userId, CancellationToken cancellationToken = default)
    {
        var cart = await session.LoadAsync<ShoppingCart>(userId, cancellationToken);
        if (cart == null)
        {
            throw new CartNotFoundException(userId.ToString());
        }
        session.Delete<ShoppingCart>(userId);
        await session.SaveChangesAsync();
        return true;
    }

    public async Task<ShoppingCart> GetCart(Guid userId, CancellationToken cancellationToken = default)
    {
        var cart = await session.LoadAsync<ShoppingCart>(userId, cancellationToken);
        return cart is null ? throw new CartNotFoundException(userId.ToString()) : cart;
    }

    public async Task<ShoppingCart> StoreCart(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        session.Store(cart);
        await session.SaveChangesAsync(cancellationToken);
        return cart;
    }
}