using Microsoft.EntityFrameworkCore;
namespace Ordering.Application.Abstractions.Data;


public interface IApplicationDbContext
{
    DbSet<Order> Orders { get; }
    DbSet<OrderItem> OrderItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}