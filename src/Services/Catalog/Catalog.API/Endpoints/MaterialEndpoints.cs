using Catalog.Application.Materials.Commands.CreateMaterial;
using Catalog.Application.Materials.Queries;

namespace Catalog.API.Endpoints;


public static class MaterialEndpoints
{

    public record CreateMaterialRequest(string Name);
    public record CreateMaterialResponse(Guid Id);

    public static void MapMaterialEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/materials", async (CreateMaterialRequest request, ISender sender) =>
        {
            // Adapter la requête en commande
            var command = request.Adapt<CreateMaterialCommand>();

            // Envoyer la commande via MediatR
            var result = await sender.Send(command);

            // Adapter le résultat en réponse
            var response = result.Adapt<CreateMaterialResponse>();

            // Retourner la réponse avec un statut 201 Created
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
    }

}