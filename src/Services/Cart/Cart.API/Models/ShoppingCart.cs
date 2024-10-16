﻿namespace Cart.API.Models;
public class ShoppingCart
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; } = default!;
    public List<ShoppingCartItem> Items { get; set; } = new();

    private decimal? _price;
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

    public ShoppingCart(Guid userId)
    {
        UserId = userId;
    }

    //Required for Mapping
    public ShoppingCart()
    {
    }
}