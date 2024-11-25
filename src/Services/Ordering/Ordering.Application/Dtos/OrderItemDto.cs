namespace Ordering.Application.Dtos;

public record OrderItemDto(
    Guid ProductId, 
    string ProductName, 
    int Quantity, 
    string Color, 
    string? Size, 
    decimal Price, 
    string Slug,
    string? CouponCode,
    string? DiscountDescription,
    decimal? OriginalPrice,
    decimal? DiscountedPrice,
    decimal? DiscountAmount,
    string? DiscountType,
    string? DiscountLabel
    );