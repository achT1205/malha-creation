namespace Catalog.Application.Categories.Queries;

public record GetCategoriesQuery : IQuery<GetCategoriesResult>;
public record GetCategoriesResult(IEnumerable<CategoryDto> Categories);

public record CategoryDto(
    Guid Id,
    string Name
    );

public class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, GetCategoriesResult>
{
    private readonly ICategoryRepository _CategoryRepository;
    public GetCategoriesQueryHandler(ICategoryRepository CategoryRepository)
    {
        _CategoryRepository = CategoryRepository;
    }
    public async Task<GetCategoriesResult> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _CategoryRepository.GetAllAsync();
        var dtos = categories.Select(x => new CategoryDto(x.Id.Value, x.Name.Value)).ToList();
        return new GetCategoriesResult(dtos);
    }
}
