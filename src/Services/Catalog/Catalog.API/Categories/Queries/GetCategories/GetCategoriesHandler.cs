namespace Catalog.API.Categories.Queries.GetCategories;
public record GetCategoriesQuery() : IQuery<GetCategoriesResult>;
public record GetCategoriesResult(IEnumerable<Category> Categories);
public class GetCategoriesQueryHandler(IDocumentSession session) : IQueryHandler<GetCategoriesQuery, GetCategoriesResult>
{
    public async Task<GetCategoriesResult> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await session.Query<Category>().ToListAsync(cancellationToken);

        return new GetCategoriesResult(categories);
    }
}