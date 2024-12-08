using Discount.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.Infrastructure.Extentions;

public static class DatabaseExtentions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedAsync(context);
    }

    private static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedCouponsAsync(context);
    }

    private static async Task SeedCouponsAsync(ApplicationDbContext context)
    {
        if (!await context.Coupons.AnyAsync())
        {
            await context.Coupons.AddRangeAsync(InitialData.Coupons);
            await context.SaveChangesAsync();
        }
    }
}