namespace Catalog.API.Products.Queries.GeProductBySlug;
public record GeProductBySlugQuery(string Slug) : IQuery<GeProductBySlugResult>;
public record GeProductBySlugResult(Product Product);
public class GeProductBySlugQueryHandler(IDocumentSession session) : IQueryHandler<GeProductBySlugQuery, GeProductBySlugResult>
{
    public async Task<GeProductBySlugResult> Handle(GeProductBySlugQuery query, CancellationToken cancellationToken)
    {
        var Product = await session.Query<Product>()
            .Where(_ => _.ColorVariants.Any(x => x.Slug == query.Slug)).FirstOrDefaultAsync();
        if (Product == null)
        {
            throw new ProductNotFoundException($"Product with the slug {query.Slug} not found!");
        }
        return new GeProductBySlugResult(Product);
    }
}