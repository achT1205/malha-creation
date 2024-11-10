using Catalog.Application.Materials.Commands.DeleteMaterial;
using Catalog.Application.Materials.Commands.CreateMaterial;
using Catalog.Application.Materials.Queries;

namespace Catalog.API.Endpoints;


public static class MaterialEndpoints
{

    public record CreateMaterialRequest(string Name);
    public record CreateMaterialResponse(Guid Id);
    public record DeleteMaterialResponse(bool IsSuccess);

    public static void MapMaterialEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/materials", async (CreateMaterialRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateMaterialCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateMaterialResponse>();
            return Results.Created($"/api/materials/{result.Id}", response);
        })
        .WithName("CreateMaterial")
        .Produces<CreateMaterialResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Material")
        .WithDescription("Create a new Material.");

        app.MapGet("/api/materials", async (ISender sender) =>
        {
            var query = new GetMaterialsQuery();
            var result = await sender.Send(query);
            return Results.Ok(result);
        })
       .WithName("GetMaterials")
       .Produces<List<MaterialDto>>(StatusCodes.Status200OK)
       .ProducesProblem(StatusCodes.Status400BadRequest)
       .WithSummary("Get materials")
       .WithDescription("Retrieve a list of all available materials.");

        app.MapDelete("/api/materials/{id}", async (Guid id, ISender sender) =>
        {
            var query = new DeleteMaterialCommand(id);
            var result = await sender.Send(query);
            var response = result.Adapt<DeleteMaterialResponse>();
            return Results.Ok(response);
        })
        .WithName("DeleteMaterialResponse")
        .Produces<DeleteMaterialResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Material")
        .WithDescription("Delete Material.");
    }

}

