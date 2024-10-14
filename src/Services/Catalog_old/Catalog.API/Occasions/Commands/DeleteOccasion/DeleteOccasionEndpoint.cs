namespace Catalog.API.Occasions.Commands.DeleteOccasion;
public record DeleteOccasionResponse(bool IsSuccess);
public class DeleteOccasionEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/occasions/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteOccasionCommand(id));

            var response = result.Adapt<DeleteOccasionResponse>();
            return Results.Ok(response);
        })
        .WithName("DeleteOccasion")
        .Produces<DeleteOccasionResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("delete Occasion")
        .WithDescription("delete Occasion");
    }
}