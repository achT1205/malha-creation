namespace Ordering.Application.Dtos;

public record OrderDto(
    Guid Id,
    Guid CustomerId,
    string OrderCode,
    AddressDto ShippingAddress,
    AddressDto BillingAddress,
    PaymentDto Payment,
    OrderStatus Status,
    List<OrderItemDto> OrderItems,
    decimal? TotalPrice,
    string? CouponCode,
    string? DiscountDescription,
    decimal? OriginalPrice,
    decimal? DiscountedPrice,
    decimal? DiscountAmount,
    string? DiscountType,
    string? DiscountLabel,
    string? PaymentIntentId,
    string? StripeSessionId);
public record OrderStockDto(
    Guid Id,
    List<OrderItemStockDto> OrderItems);

public record OrderItemStockDto(Guid ProductId, int Quantity, string Color, string Size);

