namespace Discount.Application.Coupons.Queries;

public record GetBasketDiscountQuery : IQuery<GetBasketDiscountResult>
{
    public string CouponCode { get; init; } = string.Empty; // The coupon code
    public Guid? CustomerId { get; init; } // The ID of the customer
    public decimal OrderTotal { get; init; } // The total price of the basket
    //public bool IsFirstOrder { get; init; } // Whether this is the customer's first order
    //public int RedemptionCount { get; init; } // Number of times the customer has redeemed this coupon
}


public record GetBasketDiscountResult
{
    public Guid Id { get; init; }
    public string? CouponCode { get; init; } = string.Empty; // The coupon code
    public string? Description { get; init; } = string.Empty; // Coupon description
    public decimal OriginalOrderTotal { get; init; } // The original order total
    public decimal DiscountedPrice { get; init; } 
    public decimal DiscountAmount { get; init; } // The calculated discount amount
    public decimal DiscountedOrderTotal { get; init; } // The order total after applying the discount
    public string DiscountType { get; init; } = string.Empty; // FlatAmount or Percentage
    public string DiscountLabel { get; init; } = string.Empty;
}


public class GetBasketDiscountQueryHandler (IApplicationDbContext dbContext) : IQueryHandler<GetBasketDiscountQuery, GetBasketDiscountResult>
{
    public async Task<GetBasketDiscountResult> Handle(GetBasketDiscountQuery query, CancellationToken cancellationToken)
    {
        var coupon = await dbContext.Coupons
            .Include(c => c.AllowedCustomerIds)
            .FirstOrDefaultAsync(c => c.Code.Value == query.CouponCode, cancellationToken);
        if (coupon == null)
        {
            return new GetBasketDiscountResult
            {
                CouponCode = string.Empty,
                Description = string.Empty,
                OriginalOrderTotal = query.OrderTotal,
                DiscountAmount = 0,
                DiscountedOrderTotal = query.OrderTotal,
            };
        }

        string discountType = coupon.Discountable.FlatAmount.HasValue ? "FlatAmount" : "Percentage";
        string discountLabel = coupon.Discountable.FlatAmount.HasValue
            ? $"{coupon.Discountable.FlatAmount.Value:C}"
            : $"{coupon.Discountable.Percentage.Value}%";

        var userid = query.CustomerId.HasValue ? CustomerId.Of(query.CustomerId.Value) : CustomerId.Of(new Guid());
        var discountAmount = coupon.CalculateBasketDiscount(
               userid,
               query.OrderTotal,
               false,//query.IsFirstOrder,
               0//query.RedemptionCount
           );

        var discountedOrderTotal = Math.Max(0, query.OrderTotal - discountAmount);

        return new GetBasketDiscountResult
        {
            Id = coupon.Id.Value,
            CouponCode = coupon.Code.Value,
            Description = coupon.Description,
            OriginalOrderTotal = query.OrderTotal,
            DiscountAmount = discountAmount,
            DiscountedOrderTotal = discountedOrderTotal,
            DiscountType = discountType,
            DiscountLabel = discountLabel,
        };

    }
}
