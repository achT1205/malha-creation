using Discount.Application.Data;
using Discount.Domain.Models;
using System.Reflection;

namespace Discount.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options) { }

    public DbSet<Coupon> Coupons { get; set; } = default!;
    //public DbSet<CouponProduct> CouponProducts { get; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}