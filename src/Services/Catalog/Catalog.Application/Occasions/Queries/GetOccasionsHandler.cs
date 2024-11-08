namespace Catalog.Application.Occasions.Queries;

public record GetOccasionsQuery : IQuery<GetOccasionsResult>;
public record GetOccasionsResult(IEnumerable<OccasionDto> Occasions);

public record OccasionDto(
    Guid Id,
    string Name
    );

public class GetOccasionsQueryHandler : IQueryHandler<GetOccasionsQuery, GetOccasionsResult>
{
    private readonly IOccasionRepository _OccasionRepository;
    public GetOccasionsQueryHandler(IOccasionRepository OccasionRepository)
    {
        _OccasionRepository = OccasionRepository;
    }
    public async Task<GetOccasionsResult> Handle(GetOccasionsQuery request, CancellationToken cancellationToken)
    {
        var occasions = await _OccasionRepository.GetAllAsync();
        var dtos = occasions.Select(x => new OccasionDto(x.Id.Value, x.Name.Value)).ToList();
        return new GetOccasionsResult(dtos);
    }
}
