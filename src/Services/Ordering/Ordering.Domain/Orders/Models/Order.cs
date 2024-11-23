using Ordering.Domain.Orders.AllowedTransitions;
using Ordering.Domain.Orders.Enums;

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
    public string? PaymentIntentId { get; private set; }
    public string? StripeSessionId { get; private set; }
    public string? CouponCode { get; private set; } = string.Empty; // The coupon code
    public string? DiscountDescription { get; private set; } = string.Empty; // Coupon description
    public decimal? OriginalPrice { get; private set; } // The original product price
    public decimal? DiscountedPrice { get; private set; } // The discounted product price
    public decimal? DiscountAmount { get; private set; } // The calculated discount amount
    public string? DiscountType { get; private set; } = string.Empty; // FlatAmount or Percentage
    public string? DiscountLabel { get; private set; } = string.Empty;

    public decimal TotalPrice
    {
        get => _orderItems.Sum(x => x.Price * x.Quantity);
        private set { }
    }

    public static Order Create(
        OrderId id, 
        CustomerId customerId, 
        OrderCode OrderCode, 
        Address shippingAddress, 
        Address billingAddress, 
        Payment payment,
        string? couponCode,
        string? description,
        decimal? originalPrice,
        decimal? discountedPrice,
        decimal? discountAmount,
        string? discountType,
        string? discountLabel)
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
            CreatedAt = DateTime.Now,
            GracePeriodEnd = DateTime.Now.AddMinutes(2),
            CouponCode = couponCode,
            DiscountDescription = description,
            DiscountType = discountType,
            DiscountLabel = discountLabel,
            OriginalPrice = originalPrice,
            DiscountAmount = discountAmount,
            DiscountedPrice = discountedPrice
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
        LastModified = DateTime.Now;

        AddDomainEvent(new OrderStatusChangedEvent(this, newStatus));
    }


    // Draft -> Pending
    public void SubmitForProcessing()
    {
        if (Status != OrderStatus.Draft)
            throw new InvalidOperationException("Order must be in Draft to submit for processing.");

        TransitionToStatus(OrderStatus.Pending);
    }

    // Pending -> Confirmed
    public void ConfirmOrder()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Order must be in Pending to confirm.");

        TransitionToStatus(OrderStatus.Confirmed);
    }

    // Confirmed -> GracePeriodConfirmed
    public void ConfirmGracePeriod()
    {
        if (Status != OrderStatus.Confirmed)
            throw new InvalidOperationException("Order must be Confirmed to enter Grace Period.");

        TransitionToStatus(OrderStatus.GracePeriodConfirmed);
    }

    // GracePeriodConfirmed -> Validated
    public void ValidateOrder()
    {
        if (Status != OrderStatus.GracePeriodConfirmed)
            throw new InvalidOperationException("Order must be in GracePeriodConfirmed to validate.");

        TransitionToStatus(OrderStatus.Validated);
    }

    // Validated -> Paid
    public void MarkAsPaid()
    {
        if (Status != OrderStatus.Validated)
            throw new InvalidOperationException("Order must be Validated to mark as Paid.");

        TransitionToStatus(OrderStatus.Paid);
    }

    // Paid -> Processing
    public void StartProcessing()
    {
        if (Status != OrderStatus.Paid)
            throw new InvalidOperationException("Order must be Paid to start Processing.");

        TransitionToStatus(OrderStatus.Processing);
    }

    // Processing -> StockConfirmed
    public void ConfirmStock()
    {
        if (Status != OrderStatus.Processing)
            throw new InvalidOperationException("Order must be in Processing to confirm stock.");

        TransitionToStatus(OrderStatus.StockConfirmed);
    }

    // StockConfirmed -> Packing
    public void StartPacking()
    {
        if (Status != OrderStatus.StockConfirmed)
            throw new InvalidOperationException("Order must be StockConfirmed to start Packing.");

        TransitionToStatus(OrderStatus.Packing);
    }

    // Packing -> Shipped
    public void ShipOrder()
    {
        if (Status != OrderStatus.Packing)
            throw new InvalidOperationException("Order must be in Packing to ship.");

        TransitionToStatus(OrderStatus.Shipped);
    }

    // Shipped -> OutForDelivery
    public void MarkOutForDelivery()
    {
        if (Status != OrderStatus.Shipped)
            throw new InvalidOperationException("Order must be Shipped to be Out For Delivery.");

        TransitionToStatus(OrderStatus.OutForDelivery);
    }

    // OutForDelivery -> Delivered
    public void Deliver()
    {
        if (Status != OrderStatus.OutForDelivery)
            throw new InvalidOperationException("Order must be Out For Delivery to mark as Delivered.");

        TransitionToStatus(OrderStatus.Delivered);
    }

    // Delivered -> Completed
    public void CompleteOrder()
    {
        if (Status != OrderStatus.Delivered)
            throw new InvalidOperationException("Order must be Delivered to mark as Completed.");

        TransitionToStatus(OrderStatus.Completed);
    }

    // Various statuses -> Cancelled
    public void CancelOrder()
    {
        if (Status == OrderStatus.Completed || Status == OrderStatus.Cancelled || Status == OrderStatus.Deleted)
            throw new InvalidOperationException("Order cannot be cancelled from the current status.");

        TransitionToStatus(OrderStatus.Cancelled);
    }

    // Transition to Failed from specific statuses
    public void FailOrder()
    {
        if (Status != OrderStatus.Validated && Status != OrderStatus.Pending)
            throw new InvalidOperationException("Order can only fail if it is in Pending or Validated status.");

        TransitionToStatus(OrderStatus.Failed);
    }

    // Various statuses -> Deleted
    public void DeleteOrder()
    {
        if (Status == OrderStatus.Completed || Status == OrderStatus.Deleted)
            throw new InvalidOperationException("Order cannot be deleted from the current status.");

        TransitionToStatus(OrderStatus.Deleted);
    }

    // Delivered -> RefundRequested
    public void RequestRefund()
    {
        if (Status != OrderStatus.Delivered)
            throw new InvalidOperationException("Order must be Delivered to request a refund.");

        TransitionToStatus(OrderStatus.RefundRequested);
    }

    // RefundRequested -> Refunded
    public void ProcessRefund()
    {
        if (Status != OrderStatus.RefundRequested)
            throw new InvalidOperationException("Order must be in RefundRequested to process refund.");

        TransitionToStatus(OrderStatus.Refunded);
    }

    // Shipped or Delivered -> Returned
    public void MarkAsReturned()
    {
        if (Status != OrderStatus.Shipped && Status != OrderStatus.Delivered)
            throw new InvalidOperationException("Order must be Shipped or Delivered to be marked as Returned.");

        TransitionToStatus(OrderStatus.Returned);
    }


    public void Add(
        OrderId id, 
        ProductId productId, 
        int quantity, 
        decimal price, 
        string color, 
        string size, 
        string productName, 
        string slug, 
        string? couponCode,
        string? description,
        decimal? originalPrice,
        decimal? discountedPrice,
        decimal? discountAmount,
        string? discountType,
        string? discountLabel)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var orderItem = new OrderItem(id, productId, quantity, price, color, size, productName, slug, couponCode, description, originalPrice, discountedPrice, discountAmount, discountType, discountLabel);
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
        LastModified = DateTime.Now;
        AddDomainEvent(new ShippingAddressUpdatedEvent(this));
    }

    public void UpdateBillingAddress(Address newBillingAddress)
    {
        if (Status != OrderStatus.Pending)
        {
            throw new InvalidOperationException("Cannot update billing address when the order is not in Pending status.");
        }

        BillingAddress = newBillingAddress;
        LastModified = DateTime.Now;
        AddDomainEvent(new BillingAddressUpdatedEvent(this));
    }

    public void UpdatePayment(Payment newPayment)
    {
        if (Status != OrderStatus.Pending)
        {
            throw new InvalidOperationException("Cannot update payment when the order is not in Pending status.");
        }

        Payment = newPayment;
        LastModified = DateTime.Now;
        AddDomainEvent(new PaymentUpdatedEvent(this));
    }

    public void SetStripeSessionId(string stripeSessionId)
    {
        //if (Status != OrderStatus.Pending) { 

        //}
        StripeSessionId = stripeSessionId;
    }

    public void SetPaymentIntentId()
    {
        PaymentIntentId = PaymentIntentId;
    }
}