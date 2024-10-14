namespace Catalog.API.Materials.Queries.GetMaterialById;

public record GetMaterialByIdRequest(Guid Id);
public record GetMaterialByIdResponse(Material Material);
public class GetMaterialByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/materials/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetMaterialByIdQuery(id));

            var response = result.Adapt<GetMaterialByIdResponse>();
            return Results.Ok(response);
        })
         .WithName("GetMaterialById")
         .Produces<GetMaterialByIdResponse>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Get Material by id")
         .WithDescription("Get Material by id");
    }
}