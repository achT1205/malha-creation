namespace Discount.Application.Coupons.Queries;

public record GetCouponByProductIdQuery(Guid ProductId) : IQuery<GetCouponByProductIdResult>;
public record GetCouponByProductIdResult(Coupon? Coupon);

public class GetCouponByProductIdQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetCouponByProductIdQuery, GetCouponByProductIdResult>
{
    public async Task<GetCouponByProductIdResult> Handle(GetCouponByProductIdQuery query, CancellationToken cancellationToken)
    {
        var coupon = await dbContext.Coupons
            .Include(c => c.ProductIds)
            .FirstOrDefaultAsync(c => c.ProductIds.Any(_ => _.Value == query.ProductId && c.IsDeleted == false && c.IsActive == true), cancellationToken);

        return new GetCouponByProductIdResult(coupon);
    }
}
