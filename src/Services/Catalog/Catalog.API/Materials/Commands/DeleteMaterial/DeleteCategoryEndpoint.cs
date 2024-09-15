namespace Catalog.API.Materials.Commands.DeleteMaterial;
public record DeleteMaterialResponse(bool IsSuccess);
public class DeleteMaterialEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/materials/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteMaterialCommand(id));

            var response = result.Adapt<DeleteMaterialResponse>();
            return Results.Ok(response);
        })
        .WithName("DeleteMaterial")
        .Produces<DeleteMaterialResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("delete Material")
        .WithDescription("delete Material");
    }
}