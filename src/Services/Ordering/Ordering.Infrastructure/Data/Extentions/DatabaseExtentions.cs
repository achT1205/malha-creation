using Microsoft.AspNetCore.Builder;

namespace Ordering.Infrastructure.Data.Extentions;

public static class DatabaseExtentions
{
    public static void InitialiseDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Database.MigrateAsync().GetAwaiter().GetResult();
    }
}