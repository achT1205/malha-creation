
namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand : ICommand<CreateOrderResult>
{
    public required Guid CustomerId { get; set; }
    public required AddressDto ShippingAddress { get; set; }
    public required AddressDto BillingAddress { get; set; }
    public required PaymentDto Payment { get; set; }
    public required List<OrderItemDto> OrderItems { get; set; }
}

