namespace Discount.Application.Coupons.Queries;

public record GetCouponsQuery : IQuery<GetCouponsResult>;
public record GetCouponsResult(IEnumerable<Coupon> Coupons);

public class GetCouponsQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetCouponsQuery, GetCouponsResult>
{
    public async Task<GetCouponsResult> Handle(GetCouponsQuery query, CancellationToken cancellationToken)
    {
        var coupons = await dbContext.Coupons
            .Include(c => c.AllowedCustomerIds)
            .Include(c => c.ProductIds)
            .ToArrayAsync(cancellationToken);

        return new GetCouponsResult(coupons);
    }
}
