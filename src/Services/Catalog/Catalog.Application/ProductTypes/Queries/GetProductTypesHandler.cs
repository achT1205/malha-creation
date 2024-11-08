namespace Catalog.Application.ProductTypes.Queries;

public record GetProductTypesQuery : IQuery<GetProductTypesResult>;
public record GetProductTypesResult(IEnumerable<ProductTypeDto> ProductTypes);

public record ProductTypeDto(Guid Id, string Name);

public class GetProductTypesQueryHandler : IQueryHandler<GetProductTypesQuery, GetProductTypesResult>
{
    private readonly IProductTypeRepository _productTypeRepository;
    public GetProductTypesQueryHandler(IProductTypeRepository productTypeRepository)
    {
        _productTypeRepository = productTypeRepository;
    }
    public async Task<GetProductTypesResult> Handle(GetProductTypesQuery request, CancellationToken cancellationToken)
    {
        var types = await _productTypeRepository.GetAllAsync();
        var dtos = types.Select(x => new ProductTypeDto(x.Id.Value, x.Name)).ToList();
        return new GetProductTypesResult(dtos);
    }
}
