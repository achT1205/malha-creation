namespace Catalog.API.Occasions.Commands.UpdateOccasion;
public record UpdateOccasionRequest(Occasion Occasion);
public record UpdateOccasionResponse(bool IsSuccess);
public class UpdateOccasionEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/occasions", async (UpdateOccasionRequest request, ISender sender) =>
        {

            var command = request.Adapt<UpdateOccasionCommand>();
            var result = await sender.Send(command);

            var response = result.Adapt<UpdateOccasionResponse>();
            return Results.Ok(response);
        })
         .WithName("UpdateOccasion")
         .Produces<UpdateOccasionResponse>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Update Occasion")
         .WithDescription("Update Occasion");
    }
}