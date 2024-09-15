namespace Catalog.API.Materials.Queries.GetMaterials;
public record GetMaterialsResponse(IEnumerable<Material> Materials);
public class GetMaterialsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/materials", async (ISender sender) =>
        {
            var result = await sender.Send(new GetMaterialsQuery());

            var response = result.Adapt<GetMaterialsResponse>();
            return Results.Ok(response);
        })
    .WithName("GetMaterials")
    .Produces<GetMaterialsResponse>(StatusCodes.Status201Created)
    .ProducesProblem(StatusCodes.Status400BadRequest)
    .WithSummary("Get Materials")
    .WithDescription("Get Materials");
    }
}