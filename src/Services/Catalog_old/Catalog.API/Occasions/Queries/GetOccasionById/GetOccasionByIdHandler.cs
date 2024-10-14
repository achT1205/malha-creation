namespace Catalog.API.Occasions.Queries.GetOccasionById;
public record GetOccasionByIdQuery (Guid Id): IQuery<GetOccasionByIdResult>;
public record GetOccasionByIdResult(Occasion Occasion);
public class GetOccasionByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetOccasionByIdQuery, GetOccasionByIdResult>
{
    public async Task<GetOccasionByIdResult> Handle(GetOccasionByIdQuery query, CancellationToken cancellationToken)
    {
        var Occasion = await session.LoadAsync<Occasion>(query.Id, cancellationToken);
        if (Occasion == null)
        {
            throw new OccasionNotFoundException(query.Id);
        }
        return new GetOccasionByIdResult(Occasion);
    }
}