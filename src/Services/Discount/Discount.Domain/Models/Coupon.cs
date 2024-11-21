using Discount.Domain.Abstractions;

namespace Discount.Domain.Models;

public class Coupon : Aggregate<CouponId>
{
    public CouponCode Code { get; private set; } = default!;
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Discountable Discountable { get; private set; } = default!;
    public DateTime? StartDate { get; private set; } = default!;
    public DateTime? EndDate { get; private set; } = default!;
    public int? MaxUses { get; private set; } = default!;
    public int? TotalRedemptions { get; private set; } = default!;
    public int? MaxUsesPerCustomer { get; private set; } = default!;
    public decimal? MinimumOrderValue { get; private set; } = default!;
    public bool IsFirstTimeOrderOnly { get; private set; } = default!;
    public bool IsActive { get; private set; }
    public bool IsDeleted { get; private set; }

    private readonly List<CustomerId> _allowedCustomerIds = new();
    public IReadOnlyList<CustomerId> AllowedCustomerIds => _allowedCustomerIds.AsReadOnly();

    private readonly List<ProductId> _productIds = new();
    public IReadOnlyList<ProductId> ProductIds => _productIds.AsReadOnly();

    // Constructor for new coupon creation
    private Coupon() { }

    private Coupon(
        CouponCode code,
        string name,
        string description,
        Discountable discountable,
        DateTime? startDate,
        DateTime? endDate,
        int? maxUses,
        int? maxUsesPerCustomer,
        decimal? minimumOrderValue,
        bool isFirstTimeOrderOnly,
        bool isActive)
    {
        // Validate invariants
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be null or empty.", nameof(name));
        if (discountable == null) throw new ArgumentNullException(nameof(discountable), "Discountable must be provided.");
        if (maxUses < 0) throw new ArgumentException("Max uses cannot be negative.", nameof(maxUses));
        if (maxUsesPerCustomer < 0) throw new ArgumentException("Max uses per customer cannot be negative.", nameof(maxUsesPerCustomer));

        Id = CouponId.Of(Guid.NewGuid());
        Code = code;
        Name = name;
        Description = description;
        Discountable = discountable;
        StartDate = startDate;
        EndDate = endDate;
        MaxUses = maxUses;
        MaxUsesPerCustomer = maxUsesPerCustomer;
        MinimumOrderValue = minimumOrderValue;
        IsFirstTimeOrderOnly = isFirstTimeOrderOnly;
        IsActive = isActive;
        TotalRedemptions = 0; // Start with zero redemptions
    }


    public static Coupon Create(
        CouponCode code,
        string name,
        string description,
        Discountable discountable,
        DateTime? startDate,
        DateTime? endDate,
        int? maxUses,
        int? maxUsesPerCustomer,
        decimal? minimumOrderValue,
        bool isFirstTimeOrderOnly,
        bool isActive)
    {
        if (startDate.HasValue && endDate.HasValue && startDate > endDate)
            throw new InvalidOperationException("Start date cannot be after end date.");
        if (maxUses.HasValue && maxUses < 0)
            throw new InvalidOperationException("Max uses cannot be negative.");
        if (maxUsesPerCustomer.HasValue && maxUsesPerCustomer < 0)
            throw new InvalidOperationException("Max uses per customer cannot be negative.");


        return new Coupon(
            code,
            name,
            description,
            discountable,
            startDate,
            endDate,
            maxUses,
            maxUsesPerCustomer,
            minimumOrderValue,
            isFirstTimeOrderOnly,
            isActive);
    }

    public void AddApplicableProduct(ProductId productId)
    {
        if (!_productIds.Any(id => id == productId))
            _productIds.Add(productId);
    }

    public void AddSpecificCustomer(CustomerId customerId)
    {
        if (!_allowedCustomerIds.Any(id => id == customerId))
            _allowedCustomerIds.Add(customerId);
    }

    public void RemoveApplicableProduct(ProductId productId)
    {
        if (_productIds.Any(id => id == productId))
            _productIds.Remove(productId);
    }

    public void RemoveSpecificCustomer(CustomerId customerId)
    {
        if (_allowedCustomerIds.Any(id => id == customerId))
            _allowedCustomerIds.Remove(customerId);
    }


    public decimal CalculateBasketDiscount(CustomerId customerId, decimal orderTotal, bool isFirstOrder, int redemptionCount)
    {
        if (!IsActive)
            throw new InvalidOperationException("Coupon is not active.");

        if (StartDate.HasValue && StartDate > DateTime.UtcNow)
            throw new InvalidOperationException("Coupon is not valid yet.");

        if (EndDate.HasValue && EndDate < DateTime.UtcNow)
            throw new InvalidOperationException("Coupon has expired.");

        if (MaxUses.HasValue && TotalRedemptions >= MaxUses.Value)
            throw new InvalidOperationException("Coupon usage limit has been reached.");

        if (MaxUsesPerCustomer.HasValue && redemptionCount >= MaxUsesPerCustomer.Value)
            throw new InvalidOperationException("Customer usage limit has been reached.");

        if (MinimumOrderValue.HasValue && orderTotal < MinimumOrderValue.Value)
            throw new InvalidOperationException($"Order total must be at least {MinimumOrderValue.Value:C}.");

        if (IsFirstTimeOrderOnly && !isFirstOrder)
            throw new InvalidOperationException("This coupon is valid only for first-time orders.");

        var discountAmount = Discountable.CalculateDiscount(orderTotal);

        return discountAmount;
    }

    public decimal CalculateProductDiscount(decimal productPrice)
    {
        // Calculate the discount using the Discountable value object
        var discountAmount = Discountable.CalculateDiscount(productPrice);

        // Ensure the discounted price is not negative
        var discountedPrice = Math.Max(0, productPrice - discountAmount);

        return discountedPrice;
    }


    private int GetCustomerUsageCount(Guid customerId)
    {
        // Implement logic to track and retrieve customer-specific usage counts
        return 0; // Placeholder
    }

    public void Deactivate()
    {
        IsActive = false;
    }


    public void UpdateDetails(
    string name,
    string description,
    Discountable discountable,
    DateTime? startDate,
    DateTime? endDate,
    int? maxUses,
    int? maxUsesPerCustomer,
    decimal? minimumOrderValue,
    bool isFirstTimeOrderOnly)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));

        if (startDate.HasValue && endDate.HasValue && startDate > endDate)
            throw new InvalidOperationException("Start date cannot be after end date.");

        if (maxUses.HasValue && maxUses < 0)
            throw new InvalidOperationException("Max uses cannot be negative.");

        if (maxUsesPerCustomer.HasValue && maxUsesPerCustomer < 0)
            throw new InvalidOperationException("Max uses per customer cannot be negative.");

        Name = name;
        Description = description;
        Discountable = discountable;
        StartDate = startDate;
        EndDate = endDate;
        MaxUses = maxUses;
        MaxUsesPerCustomer = maxUsesPerCustomer;
        MinimumOrderValue = minimumOrderValue;
        IsFirstTimeOrderOnly = isFirstTimeOrderOnly;
    }

    public void Delete()
    {
        if (IsDeleted)
            throw new InvalidOperationException("Coupon is already deleted.");

        IsDeleted = true;
    }

}
