using Catalog.API.Services.Interfaces;

namespace Catalog.API.Products.Queries.GeProductBySlug;
public record GeProductBySlugQuery(string Slug) : IQuery<GeProductBySlugResult>;
public record GeProductBySlugResult(Product Product);
public class GeProductBySlugQueryHandler(IDocumentSession session, ISctockApiService sctockApiService) : IQueryHandler<GeProductBySlugQuery, GeProductBySlugResult>
{
    public async Task<GeProductBySlugResult> Handle(GeProductBySlugQuery query, CancellationToken cancellationToken)
    {
        var product = await session.Query<Product>()
            .Where(_ => _.ColorVariants.Any(x => x.Slug == query.Slug)).FirstOrDefaultAsync();
        if (product == null)
        {
            throw new ProductNotFoundException($"Product with the slug {query.Slug} not found!");
        }

        var stock = await sctockApiService.GetStockByProductIdAsync(product.Id);
        if (stock == null)
        {
            throw new NoRelatedStockFoundException($"No stock found for the product {product.Id}");
        }

        foreach (var variant in product.ColorVariants)
        {
            if (product.ProductType == ProductType.Clothing)
            {
                foreach (var size in variant?.Sizes ?? Enumerable.Empty<SizeVariant>())
                {
                    var stockVariant = stock?.ColorVariants?.FirstOrDefault(i => i.Color == variant?.Color && i.Size == size.Size);
                    size.Quantity = stockVariant?.Quantity ?? 0;  // Assign default value (0) if stock or quantity is null
                }
            }
            else
            {
                var stockVariant = stock?.ColorVariants?.FirstOrDefault(i => i.Color == variant.Color);
                variant.Quantity = stockVariant?.Quantity ?? 0;  // Assign default value (0) if stock or quantity is null
            }
        }
        return new GeProductBySlugResult(product);
    }
}