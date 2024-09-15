namespace Catalog.API.Materials.Queries.GetMaterials;
public record GetMaterialsQuery() : IQuery<GetMaterialsResult>;
public record GetMaterialsResult(IEnumerable<Material> Materials);
public class GetMaterialsQueryHandler(IDocumentSession session) : IQueryHandler<GetMaterialsQuery, GetMaterialsResult>
{
    public async Task<GetMaterialsResult> Handle(GetMaterialsQuery request, CancellationToken cancellationToken)
    {
        var Materials = await session.Query<Material>().ToListAsync(cancellationToken);

        return new GetMaterialsResult(Materials);
    }
}