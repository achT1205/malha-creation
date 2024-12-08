namespace Discount.Application.Coupons.Queries;

public record GetCouponQuery(Guid Id) : IQuery<GetCouponResult>;
public record GetCouponResult(Coupon Coupon);

public class GetCouponQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetCouponQuery, GetCouponResult>
{
    public async Task<GetCouponResult> Handle(GetCouponQuery query, CancellationToken cancellationToken)
    {
        var coupon = await dbContext.Coupons
            .Include(c => c.AllowedCustomerIds)
            .Include(c => c.ProductIds)
            .FirstOrDefaultAsync(_ => _.Id == CouponId.Of(query.Id), cancellationToken);

        if (coupon == null)
            throw new NotFoundException($"Coupon with ID {query.Id} not found.");

        return new GetCouponResult(coupon);
    }
}
