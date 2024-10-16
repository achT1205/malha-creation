
using BuildingBlocks.CQRS;

namespace Catalog.Application.Products.Queries.GetProducts;

public record GetProductsQuery : IQuery<GetProductsQueryResult>;