namespace Catalog.API.Materials.Commands.UpdateMaterial;
public record UpdateMaterialRequest(Material Material);
public record UpdateMaterialResponse(bool IsSuccess);
public class UpdateMaterialEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/materials", async (UpdateMaterialRequest request, ISender sender) =>
        {

            var command = request.Adapt<UpdateMaterialCommand>();
            var result = await sender.Send(command);

            var response = result.Adapt<UpdateMaterialResponse>();
            return Results.Ok(response);
        })
         .WithName("UpdateMaterial")
         .Produces<UpdateMaterialResponse>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Update Material")
         .WithDescription("Update Material");
    }
}