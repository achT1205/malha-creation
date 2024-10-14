namespace Catalog.API.Materials.Commands.CreateMaterial;
public record CreateMaterialRequest(MaterialDto  Material);
public record CreateMaterialResponse(Guid Id);
public class CreateMaterialEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/materials", async (CreateMaterialRequest request, ISender sender) =>
        {

            var command = request.Adapt<CreateMaterialCommand>();
            var result = await sender.Send(command);

            var response = result.Adapt<CreateMaterialResponse>();
            return Results.Created($"/materials/{response.Id}", response);
        })
         .WithName("CreateMaterial")
         .Produces<CreateMaterialResponse>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Create Material")
         .WithDescription("Create Material");
    }
}