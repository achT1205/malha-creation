namespace Catalog.API.Materials.Queries.GetMaterialById;
public record GetMaterialByIdQuery (Guid Id): IQuery<GetMaterialByIdResult>;
public record GetMaterialByIdResult(Material Material);
public class GetMaterialByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetMaterialByIdQuery, GetMaterialByIdResult>
{
    public async Task<GetMaterialByIdResult> Handle(GetMaterialByIdQuery query, CancellationToken cancellationToken)
    {
        var Material = await session.LoadAsync<Material>(query.Id, cancellationToken);
        if (Material == null)
        {
            throw new MaterialNotFoundException(query.Id);
        }
        return new GetMaterialByIdResult(Material);
    }
}