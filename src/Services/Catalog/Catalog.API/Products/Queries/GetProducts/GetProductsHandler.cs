namespace Catalog.API.Products.Queries.GetProducts;
public record GetProductsQuery() : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);
public class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var Products = await session.Query<Product>().ToListAsync(cancellationToken);

        return new GetProductsResult(Products);
    }
}