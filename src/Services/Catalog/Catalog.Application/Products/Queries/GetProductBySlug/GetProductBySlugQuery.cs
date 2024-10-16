using BuildingBlocks.CQRS;

namespace Catalog.Application.Products.Queries.GetProductBySlug;
public record GetProductBySlugQuery(string Slug) : IQuery<GetProductBySlugQueryResult>;