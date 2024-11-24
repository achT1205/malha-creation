namespace BuildingBlocks.Messaging.Events;

public record CartCheckoutEvent : IntegrationEvent
{
    public Guid NewOrderId { get; set; }
    public OrderBasket Basket { get; set; } = default!;
    public AddressDto ShippingAddress { get; set; } = default!;
    public AddressDto BillingAddress { get; set; } = default!;
    public PaymentDto Payment { get; set; } = default!;
}
