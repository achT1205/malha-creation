﻿namespace Catalog.Application.Materials.Queries;

public record GetMaterialsQuery : IQuery<GetMaterialsResult>;
public record GetMaterialsResult(IEnumerable<MaterialDto> Materials);

public class GetMaterialsQueryHandler : IQueryHandler<GetMaterialsQuery, GetMaterialsResult>
{
    private readonly IMaterialRepository _materialRepository;
    public GetMaterialsQueryHandler(IMaterialRepository materialRepository)
    {
        _materialRepository = materialRepository;
    }
    public async Task<GetMaterialsResult> Handle(GetMaterialsQuery request, CancellationToken cancellationToken)
    {
        var types = await _materialRepository.GetAllAsync();
        var dtos = types.Select(x => new MaterialDto(x.Id.Value, x.Name, x.Description)).ToList();
        return new GetMaterialsResult(dtos);
    }
}
