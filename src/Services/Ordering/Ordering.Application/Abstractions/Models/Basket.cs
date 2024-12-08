namespace Ordering.Application.Abstractions.Models;

public record Basket(
    Guid Id, 
    Guid UserId, 
    List<BasketItem> Items);
public record BasketItem(
    Guid ProductId,
    string ProductName,
    Guid ColorVariantId, 
    Guid? SizeVariantId,
    int Quantity,
    string Color,
    string Size,
    decimal Price,
    string Slug);