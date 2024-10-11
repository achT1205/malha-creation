namespace Ordering.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand
    : ICommand<UpdateOrderResult>
{
    public Guid Id { get; set; }
    public required AddressDto ShippingAddress { get; set; }
    public required AddressDto BillingAddress { get; set; }
    public required PaymentDto Payment { get; set; }
}
