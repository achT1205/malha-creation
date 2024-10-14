namespace Catalog.API.Occasions.Queries.GetOccasionById;

public record GetOccasionByIdRequest(Guid Id);
public record GetOccasionByIdResponse(Occasion Occasion);
public class GetMaterialByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/occasions/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetOccasionByIdQuery(id));

            var response = result.Adapt<GetOccasionByIdResponse>();
            return Results.Ok(response);
        })
         .WithName("GetOccasionById")
         .Produces<GetOccasionByIdResponse>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Get Occasion by id")
         .WithDescription("Get Occasion by id");
    }
}