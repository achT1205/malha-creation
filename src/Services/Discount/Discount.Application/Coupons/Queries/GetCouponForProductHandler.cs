namespace Discount.Application.Coupons.Queries;

public record GetCouponForProductQuery : IQuery<GetCouponForProductResult>
{
    public Guid ProductId { get; init; }
    public decimal ProductPrice { get; init; }
}

public record GetCouponForProductResult
{
    public string CouponCode { get; init; } = string.Empty; // The coupon code
    public string Description { get; init; } = string.Empty; // Coupon description
    public decimal OriginalPrice { get; init; } // The original product price
    public decimal DiscountedPrice { get; init; } // The discounted product price
    public decimal DiscountAmount { get; init; } // The calculated discount amount
    public string DiscountType { get; init; } = string.Empty; // FlatAmount or Percentage
    public string DiscountLabel { get; init; } = string.Empty;
}


public class GetCouponForProductQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetCouponForProductQuery, GetCouponForProductResult>
{
    public async Task<GetCouponForProductResult> Handle(GetCouponForProductQuery query, CancellationToken cancellationToken)
    {

        var coupon = await dbContext.Coupons
           .Include(c => c.ProductIds)
           .FirstOrDefaultAsync(c => c.ProductIds.Any(_ => _.Value == query.ProductId && c.IsDeleted == false && c.IsActive == true), cancellationToken);

        if (coupon == null) return new GetCouponForProductResult
        {
            CouponCode = "None",
            Description = "No discount",
            OriginalPrice = query.ProductPrice,
            DiscountedPrice = query.ProductPrice,
            DiscountAmount = 0,
            DiscountType = "None",
            DiscountLabel = "None",
        };

        decimal discountAmount = 0;
        decimal discountedPrice = query.ProductPrice;
        string discountType = "None";

        discountAmount = coupon.CalculateProductDiscount(query.ProductPrice);

        discountType = coupon.Discountable.FlatAmount.HasValue ? "FlatAmount" : "Percentage";
        string discountLabel = coupon.Discountable.FlatAmount.HasValue
            ? $"{coupon.Discountable.FlatAmount.Value:C}"
            : $"{coupon.Discountable.Percentage.Value}%";

        discountedPrice = Math.Max(0, query.ProductPrice - discountAmount); // Ensure price is not negative

        return new GetCouponForProductResult
        {
            CouponCode = coupon.Code.Value,
            Description = coupon.Description,
            OriginalPrice = query.ProductPrice,
            DiscountedPrice = discountedPrice,
            DiscountAmount = discountAmount,
            DiscountType = discountType,
            DiscountLabel = discountLabel
        };
    }
}
