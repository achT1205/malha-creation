namespace Cart.API.Data;
public interface ICartRepository
{
    Task<ShoppingCart> GetCart(Guid userId, CancellationToken cancellationToken = default);
    Task<ShoppingCart> StoreCart(ShoppingCart cart, CancellationToken cancellationToken = default);
    Task<bool> DeleteCart(Guid userId, CancellationToken cancellationToken = default);
}