using Ordering.Domain.Orders.AllowedTransitions;
using Ordering.Domain.Orders.Enums;
using Ordering.Domain.Orders.Events;

namespace Ordering.Domain.Orders.Models;

public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public CustomerId CustomerId { get; private set; } = default!;
    public OrderCode OrderCode { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public DateTime GracePeriodEnd { get; private set; }

    public decimal TotalPrice
    {
        get => _orderItems.Sum(x => x.Price * x.Quantity);
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
            Status = OrderStatus.Draft,
            CreatedAt = DateTime.UtcNow,
            GracePeriodEnd = DateTime.UtcNow.AddMinutes(10)
        };
        order.AddDomainEvent(new OrderCreatedEvent(order));
        return order;
    }

    // Method to perform transitions with validation
    private void TransitionToStatus(OrderStatus newStatus)
    {
        if (!OrderStatusTransitions.CanTransition(Status, newStatus))
        {
            throw new InvalidOperationException($"Transition from {Status} to {newStatus} is not allowed.");
        }

        Status = newStatus;
        LastModified = DateTime.UtcNow;

        AddDomainEvent(new OrderStatusChangedEvent(this, newStatus));
    }

    // Draft -> Pending
    public void SubmitForProcessing()
    {
        if (Status != OrderStatus.Draft)
            throw new InvalidOperationException("Order must be in Draft to submit for processing.");

        TransitionToStatus(OrderStatus.Pending);
    }

    // Draft or Pending -> Cancelled
    public void CancelOrder()
    {
        if (Status != OrderStatus.Draft && Status != OrderStatus.Pending)
            throw new InvalidOperationException("Order can only be cancelled in Draft or Pending.");

        TransitionToStatus(OrderStatus.Cancelled);
    }

    // Draft or Pending -> Deleted
    public void DeleteOrder()
    {
        if (Status != OrderStatus.Draft && Status != OrderStatus.Pending)
            throw new InvalidOperationException("Order can only be deleted in Draft or Pending.");

        TransitionToStatus(OrderStatus.Deleted);
    }

    // Pending -> ConfirmGracePeriod
    public void ConfirmGracePeriod()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Order must be in Pending to confirm grace period.");

        TransitionToStatus(OrderStatus.GracePeriodConfirmed);
    }

    // GracePeriodConfirmed -> Validated --- implement validatios(paymet methode ....)
    public void ValidateOrder()
    {
        if (Status != OrderStatus.GracePeriodConfirmed)
            throw new InvalidOperationException("Order must be in GracePeriodConfirmed to be validated.");

        TransitionToStatus(OrderStatus.Validated);
    }

    // GracePeriodConfirmed -> Rejected
    public void RejectOrder()
    {
        if (Status != OrderStatus.GracePeriodConfirmed)
            throw new InvalidOperationException("Order must be in GracePeriodConfirmed to be rejected.");

        TransitionToStatus(OrderStatus.Rejected);
    }

    // Validated -> StockConfirmed
    public void ConfirmStock()
    {
        if (Status != OrderStatus.Validated)
            throw new InvalidOperationException("Order must be Validated before stock confirmation.");

        TransitionToStatus(OrderStatus.StockConfirmed);
    }

    // StockConfirmed -> Paid
    public void MarkAsPaid()
    {
        if (Status != OrderStatus.StockConfirmed)
            throw new InvalidOperationException("Order must be StockConfirmed to be marked as paid.");

        TransitionToStatus(OrderStatus.Paid);
    }

    // Paid -> Shipped
    public void ShipOrder()
    {
        if (Status != OrderStatus.Paid)
            throw new InvalidOperationException("Order must be Paid to be shipped.");

        TransitionToStatus(OrderStatus.Shipped);
    }

    // Shipped -> Completed
    public void CompleteOrder()
    {
        if (Status != OrderStatus.Shipped)
            throw new InvalidOperationException("Order must be Shipped to be completed.");

        TransitionToStatus(OrderStatus.Completed);
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

    public void UpdateShippingAddress(Address newShippingAddress)
    {
        if (Status != OrderStatus.Pending)
        {
            throw new InvalidOperationException("Cannot update shipping address when the order is not in Pending status.");
        }

        ShippingAddress = newShippingAddress;
        LastModified = DateTime.UtcNow;
        AddDomainEvent(new ShippingAddressUpdatedEvent(this));
    }

    public void UpdateBillingAddress(Address newBillingAddress)
    {
        if (Status != OrderStatus.Pending)
        {
            throw new InvalidOperationException("Cannot update billing address when the order is not in Pending status.");
        }

        BillingAddress = newBillingAddress;
        LastModified = DateTime.UtcNow;
        AddDomainEvent(new BillingAddressUpdatedEvent(this));
    }

    public void UpdatePayment(Payment newPayment)
    {
        if (Status != OrderStatus.Pending)
        {
            throw new InvalidOperationException("Cannot update payment when the order is not in Pending status.");
        }

        Payment = newPayment;
        LastModified = DateTime.UtcNow;
        AddDomainEvent(new PaymentUpdatedEvent(this));
    }
}