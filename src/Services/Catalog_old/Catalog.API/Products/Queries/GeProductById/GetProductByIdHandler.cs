using Catalog.API.Services.Interfaces;
using JasperFx.Core;

namespace Catalog.API.Products.Queries.GetProductById;
public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);
public class GetProductByIdQueryHandler(IDocumentSession session, ISctockApiService sctockApiService) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);
        if (product == null)
        {
            throw new ProductNotFoundException(query.Id);
        }

        var stock = await sctockApiService.GetStockByProductIdAsync(product.Id);
        if (stock == null)
        {
            throw new NoRelatedStockFoundException($"No stock found for the product {query.Id}");
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

        return new GetProductByIdResult(product);
    }
}