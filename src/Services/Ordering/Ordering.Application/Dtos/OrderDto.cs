using Ordering.Domain.Orders.Enums;

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
    decimal? TotalPrice);
