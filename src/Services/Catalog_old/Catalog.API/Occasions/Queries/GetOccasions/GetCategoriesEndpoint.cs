namespace Catalog.API.Occasions.Queries.GetOccasions;
public record GetOccasionsResponse(IEnumerable<Occasion> Occasions);
public class GetOccasionsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/occasions", async (ISender sender) =>
        {
            var result = await sender.Send(new GetOccasionsQuery());

            var response = result.Adapt<GetOccasionsResponse>();
            return Results.Ok(response);
        })
    .WithName("GetOccasions")
    .Produces<GetOccasionsResponse>(StatusCodes.Status201Created)
    .ProducesProblem(StatusCodes.Status400BadRequest)
    .WithSummary("Get Occasions")
    .WithDescription("Get Occasions");
    }
}