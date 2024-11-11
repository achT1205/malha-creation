using Catalog.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;

namespace Catalog.Infrastructure.Extentions;


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
        await SeedCategoryAsync(context);
        await SeedOccasionAsync(context);
        await SeedMaterialAsync(context);
        //await SeedProductTypeAsync(context);
        await SeedCollectionAsync(context);
        await SeedBrandsAsync(context);
        //await SeedProductsAsync(context);
    }


    private static async Task SeedCategoryAsync(ApplicationDbContext context)
    {
        if (!await context.Categories.AnyAsync())
        {
            await context.Categories.AddRangeAsync(InitialData.Categories);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedOccasionAsync(ApplicationDbContext context)
    {
        if (!await context.Occasions.AnyAsync())
        {
            await context.Occasions.AddRangeAsync(InitialData.Occasions);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedMaterialAsync(ApplicationDbContext context)
    {
        if (!await context.Materials.AnyAsync())
        {
            await context.Materials.AddRangeAsync(InitialData.Materials);
            await context.SaveChangesAsync();
        }
    }

    //private static async Task SeedProductTypeAsync(ApplicationDbContext context)
    //{
    //    if (!await context.ProductTypes.AnyAsync())
    //    {
    //        await context.ProductTypes.AddRangeAsync(InitialData.ProductTypes);
    //        await context.SaveChangesAsync();
    //    }
    //}

    private static async Task SeedCollectionAsync(ApplicationDbContext context)
    {
        if (!await context.Collections.AnyAsync())
        {
            await context.Collections.AddRangeAsync(InitialData.Collections);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedBrandsAsync(ApplicationDbContext context)
    {
        if (!await context.Brands.AnyAsync())
        {
            await context.Brands.AddRangeAsync(InitialData.Brands);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedProductsAsync(ApplicationDbContext context)
    {
        if (!await context.Products.AnyAsync())
        {
            await context.Products.AddRangeAsync(InitialData.Products);
            await context.SaveChangesAsync();
        }
    }
}