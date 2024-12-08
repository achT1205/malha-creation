namespace Catalog.Application.Collections.Queries;

public record GetCollectionsQuery : IQuery<GetCollectionsResult>;
public record GetCollectionsResult(IEnumerable<CollectionDto> Collections);

public class GetCollectionsQueryHandler : IQueryHandler<GetCollectionsQuery, GetCollectionsResult>
{
    private readonly ICollectionRepository _CollectionRepository;
    public GetCollectionsQueryHandler(ICollectionRepository CollectionRepository)
    {
        _CollectionRepository = CollectionRepository;
    }
    public async Task<GetCollectionsResult> Handle(GetCollectionsQuery request, CancellationToken cancellationToken)
    {
        var collections = await _CollectionRepository.GetAllAsync();
        var dtos = collections.Select(x => new CollectionDto(x.Id.Value, x.Name, x.Description, new ImageDto(x.CoverImage.ImageSrc, x.CoverImage.AltText))).ToList();
        return new GetCollectionsResult(dtos);
    }
}
