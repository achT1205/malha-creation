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

    // Pending -> PaymentPending
    public void StartPayment()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Order must be in Pending state to start payment.");

        TransitionToStatus(OrderStatus.PaymentPending);
    }

    // PaymentPending -> Paid
    public void CompletePayment()
    {
        if (Status != OrderStatus.PaymentPending)
            throw new InvalidOperationException("Order must be in PaymentPending state to complete payment.");

        TransitionToStatus(OrderStatus.Paid);
    }

    // Paid -> GracePeriodConfirmed
    public void ConfirmGracePeriod()
    {
        if (Status != OrderStatus.Paid)
            throw new InvalidOperationException("Order must be in Paid state to confirm grace period.");

        TransitionToStatus(OrderStatus.GracePeriodConfirmed);
    }


    // Paid -> Modified
    public void UpdateOrder()
    {
        if (Status != OrderStatus.Paid)
            throw new InvalidOperationException("Order must be in Paid state to modify.");

        TransitionToStatus(OrderStatus.Modified);
    }

    // GracePeriodConfirmed -> Processing
    public void StartProcessing()
    {
        if (Status != OrderStatus.GracePeriodConfirmed && Status != OrderStatus.Modified)
            throw new InvalidOperationException("Order must be in GracePeriodConfirmed or Modified state to start processing.");

        TransitionToStatus(OrderStatus.Processing);
    }


    // Processing -> StockConfirmed
    public void ConfirmStock()
    {
        if (Status != OrderStatus.Processing)
            throw new InvalidOperationException("Order must be in Processing state to confirm stock.");

        TransitionToStatus(OrderStatus.StockConfirmed);
    }

    // StockConfirmed -> Packing
    public void StartPacking()
    {
        if (Status != OrderStatus.StockConfirmed)
            throw new InvalidOperationException("Order must be in StockConfirmed state to start packing.");

        TransitionToStatus(OrderStatus.Packing);
    }

    // Packing -> Shipped
    public void ShipOrder()
    {
        if (Status != OrderStatus.Packing)
            throw new InvalidOperationException("Order must be in Packing state to ship.");

        TransitionToStatus(OrderStatus.Shipped);
    }

    // Shipped -> OutForDelivery
    public void MarkOutForDelivery()
    {
        if (Status != OrderStatus.Shipped)
            throw new InvalidOperationException("Order must be in Shipped state to mark as OutForDelivery.");

        TransitionToStatus(OrderStatus.OutForDelivery);
    }

    // OutForDelivery -> Delivered
    public void MarkAsDelivered()
    {
        if (Status != OrderStatus.OutForDelivery)
            throw new InvalidOperationException("Order must be in OutForDelivery state to mark as Delivered.");

        TransitionToStatus(OrderStatus.Delivered);
    }

    // AwaitingPickup -> Delivered
    public void MarkPickupAsDelivered()
    {
        if (Status != OrderStatus.AwaitingPickup)
            throw new InvalidOperationException("Order must be in AwaitingPickup state to mark as Delivered.");

        TransitionToStatus(OrderStatus.Delivered);
    }

    // Delivered -> Completed
    public void CompleteOrder()
    {
        if (Status != OrderStatus.Delivered)
            throw new InvalidOperationException("Order must be in Delivered state to complete.");

        TransitionToStatus(OrderStatus.Completed);
    }


    // Any -> Cancelled
    public void CancelOrder()
    {
        if (Status == OrderStatus.Completed || Status == OrderStatus.Cancelled)
            throw new InvalidOperationException("Order cannot be cancelled if it is already Completed or Cancelled.");

        TransitionToStatus(OrderStatus.Cancelled);
    }

    // Delivered -> Returned
    public void ReturnOrder()
    {
        if (Status != OrderStatus.Delivered)
            throw new InvalidOperationException("Order must be in Delivered state to return.");

        TransitionToStatus(OrderStatus.Returned);
    }

    // Returned -> Refunded
    public void RefundOrder()
    {
        if (Status != OrderStatus.Returned)
            throw new InvalidOperationException("Order must be in Returned state to refund.");

        TransitionToStatus(OrderStatus.Refunded);
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
        StartPayment();
        StripeSessionId = stripeSessionId;
    }

    public void SetPaymentIntentId(string paymentIntentId)
    {
        CompletePayment();
        PaymentIntentId = paymentIntentId;
        GracePeriodEnd = DateTime.Now.AddMinutes(2);
    }

    public void MarkAsFailed()
    {
        TransitionToStatus(OrderStatus.Failed);
    }
}