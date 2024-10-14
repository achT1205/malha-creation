namespace Catalog.API.Occasions.Commands.CreateOccasion;
public record CreateOccasionRequest(OccasionDto Occasion);
public record CreateOccasionResponse(Guid Id);
public class CreateMaterialEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/occasions", async (CreateOccasionRequest request, ISender sender) =>
        {

            var command = request.Adapt<CreateOccasionCommand>();
            var result = await sender.Send(command);

            var response = result.Adapt<CreateOccasionResponse>();
            return Results.Created($"/Occasions/{response.Id}", response);
        })
         .WithName("CreateOccasion")
         .Produces<CreateOccasionResponse>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Create Occasion")
         .WithDescription("Create Occasion");
    }
}