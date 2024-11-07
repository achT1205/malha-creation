namespace Cart.API.Data;
public interface ICartRepository
{
    Task<Basket> GetCart(Guid userId, CancellationToken cancellationToken = default);
    Task<Basket> StoreCart(Basket cart, CancellationToken cancellationToken = default);
    Task<bool> DeleteCart(Guid userId, CancellationToken cancellationToken = default);
}