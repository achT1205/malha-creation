namespace Catalog.API.Occasions.Queries.GetOccasions;
public record GetOccasionsQuery() : IQuery<GetOccasionsResult>;
public record GetOccasionsResult(IEnumerable<Occasion> Occasions);
public class GetOccasionsQueryHandler(IDocumentSession session) : IQueryHandler<GetOccasionsQuery, GetOccasionsResult>
{
    public async Task<GetOccasionsResult> Handle(GetOccasionsQuery request, CancellationToken cancellationToken)
    {
        var Occasions = await session.Query<Occasion>().ToListAsync(cancellationToken);

        return new GetOccasionsResult(Occasions);
    }
}