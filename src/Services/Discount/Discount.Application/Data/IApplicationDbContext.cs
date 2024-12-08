namespace Discount.Application.Data;

public interface IApplicationDbContext
{
    DbSet<Coupon> Coupons { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
