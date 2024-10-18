using Catalog.Application.Dtos;

namespace Catalog.Application.Products.Queries.GetProducts;

public record GetProductsQueryResult(IEnumerable<ProductDto> Products);

