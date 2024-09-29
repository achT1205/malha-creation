namespace Cart.API.Models;
public class ShoppingCart
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; } = default!;
    public List<ShoppingCartItem> Items { get; set; } = new();

    private decimal? _price;
    public decimal TotalPrice { get => !_price.HasValue ? Items.Sum(x => x.Price * x.Quantity) : _price.Value; }

    public ShoppingCart(Guid userId)
    {
        UserId = userId;
    }
    public void SetTotalPrice(decimal price)
    {
        _price = price;
    }

    //Required for Mapping
    public ShoppingCart()
    {
    }
}