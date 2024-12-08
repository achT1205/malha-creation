using Microsoft.Extensions.Logging;

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
    public string CouponCode { get; init; } = string.Empty; // The coupon code
    public string Description { get; init; } = string.Empty; // Coupon description
    public decimal OriginalPrice { get; init; } // The original product price
    public decimal DiscountedPrice { get; init; } // The discounted product price
    public decimal DiscountAmount { get; init; } // The calculated discount amount
    public string DiscountType { get; init; } = string.Empty; // FlatAmount or Percentage
    public string DiscountLabel { get; init; } = string.Empty;
}


public class GetBasketDiscountQueryHandler(IApplicationDbContext dbContext, ILogger<GetBasketDiscountQueryHandler> logger) : IQueryHandler<GetBasketDiscountQuery, GetBasketDiscountResult>
{
    public async Task<GetBasketDiscountResult> Handle(GetBasketDiscountQuery query, CancellationToken cancellationToken)
    {
        var coupon = await dbContext.Coupons
            .Include(c => c.AllowedCustomerIds)
            .FirstOrDefaultAsync(c => c.Code.Value == query.CouponCode && c.IsDeleted == false && c.IsActive == true, cancellationToken);
        if (coupon == null)
        {
            return new GetBasketDiscountResult
            {
                CouponCode = "None",
                Description = "No discount",
                OriginalPrice = query.OrderTotal,
                DiscountAmount = 0,
                DiscountedPrice = query.OrderTotal,
            };
        }

        string discountType = coupon.Discountable.FlatAmount.HasValue ? "FlatAmount" : "Percentage";
        string discountLabel = coupon.Discountable.FlatAmount.HasValue
            ? $"{coupon.Discountable.FlatAmount.Value:C}"
            : $"{coupon.Discountable.Percentage.Value}%";

        var userid = query.CustomerId.HasValue ? CustomerId.Of(query.CustomerId.Value) : CustomerId.Of(new Guid());
        try
        {
            var discountPrice = coupon.CalculateBasketDiscount(
               userid,
               query.OrderTotal,
               true,//query.IsFirstOrder,
               0//query.RedemptionCount
           );

            var discountedAmount = Math.Max(0, query.OrderTotal - discountPrice);

            return new GetBasketDiscountResult
            {
                CouponCode = coupon.Code.Value,
                Description = coupon.Description,
                OriginalPrice = query.OrderTotal,
                DiscountAmount = discountedAmount,
                DiscountedPrice = discountPrice,
                DiscountType = discountType,
                DiscountLabel = discountLabel,
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw;
        }

    }
}
