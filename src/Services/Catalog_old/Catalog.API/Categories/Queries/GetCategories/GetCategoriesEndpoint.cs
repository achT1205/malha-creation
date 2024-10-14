namespace Catalog.API.Categories.Queries.GetCategories;
public record GetCategoriesResponse(IEnumerable<Category> Categories);
public class GetCategoriesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/categories", async (ISender sender) =>
        {
            var result = await sender.Send(new GetCategoriesQuery());

            var response = result.Adapt<GetCategoriesResponse>();
            return Results.Ok(response);
        })
    .WithName("GetCategories")
    .Produces<GetCategoriesResponse>(StatusCodes.Status201Created)
    .ProducesProblem(StatusCodes.Status400BadRequest)
    .WithSummary("Get categories")
    .WithDescription("Get categories");
    }
}