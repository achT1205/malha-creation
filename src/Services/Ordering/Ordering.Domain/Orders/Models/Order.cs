using Ordering.Domain.Orders.Enums;
using Ordering.Domain.Orders.Events;

namespace Ordering.Domain.Orders.Models;

public class Order : Aggregate<OrderId>
{
    private readonly HashSet<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.ToList();
    public CustomerId CustomerId { get; private set; } = default!;
    public OrderCode OrderCode { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public decimal TotalPrice
    {
        get => OrderItems.Sum(x => x.Price * x.Quantity);
        private set { }
    }

    public static Order Create(OrderId id, CustomerId customerId, OrderCode OrderCode, Address shippingAddress, Address billingAddress, Payment payment)
    {
        var order = new Order
        {
            Id = id,
            CustomerId = customerId,
            OrderCode = OrderCode,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            Payment = payment,
            Status = OrderStatus.Pending,
            CreatedAt = DateTime.UtcNow,
        };
        order.AddDomainEvent(new OrderCreatedEvent(order));
        return order;
    }

    public void Update(Address shippingAddress, Address billingAddress, Payment payment)
    {
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        Status = OrderStatus.Updated;
        LastModified = DateTime.UtcNow;

        AddDomainEvent(new OrderUpdatedEvent(this));
    }

    public void Add(OrderId id, ProductId productId, int quantity, decimal price, string color, string size, string productName, string slug)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var orderItem = new OrderItem(id, productId, quantity, price, color, size, productName, slug);
        _orderItems.Add(orderItem);
    }

    public void Remove(ProductId productId)
    {
        var orderItem = _orderItems.FirstOrDefault(x => x.ProductId == productId);
        if (orderItem is not null)
        {
            _orderItems.Remove(orderItem);
        }
    }
}