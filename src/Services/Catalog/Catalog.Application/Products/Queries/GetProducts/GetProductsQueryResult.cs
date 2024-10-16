using Catalog.Application.Products.Dtos;

namespace Catalog.Application.Products.Queries.GetProducts;

public record GetProductsQueryResult(IEnumerable<ProductDto> Products);

