namespace Catalog.API.Products.Queries.GetProductById;
public record GetProductByIdQuery (Guid Id): IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);
public class GetProductByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var Product = await session.LoadAsync<Product>(query.Id, cancellationToken);
        if (Product == null)
        {
            throw new ProductNotFoundException(query.Id);
        }
        return new GetProductByIdResult(Product);
    }
}