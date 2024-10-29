using BuildingBlocks.CQRS;

namespace Catalog.Application.Products.Queries.GetProductById;
public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdQueryResult>;