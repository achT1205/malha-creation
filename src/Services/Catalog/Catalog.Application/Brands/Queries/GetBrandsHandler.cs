using BuildingBlocks.CQRS;
using Catalog.Application.Interfaces;

namespace Catalog.Application.Brands.Queries;

public record GetBrandsQuery : IQuery<GetBrandsResult>;
public record GetBrandsResult(IEnumerable<BrandDto> Brands);

public record BrandDto(Guid Id, string Name);

public class GetBrandsQueryHandler : IQueryHandler<GetBrandsQuery, GetBrandsResult>
{
    private readonly IBrandRepository _BrandRepository;
    public GetBrandsQueryHandler(IBrandRepository BrandRepository)
    {
        _BrandRepository = BrandRepository;
    }
    public async Task<GetBrandsResult> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
    {
        var Brands = await _BrandRepository.GetAllAsync();

        var dtos = Brands.Select(x => new BrandDto(x.Id.Value, x.Name.Value)).ToList();

        return new GetBrandsResult(dtos);
    }
}
