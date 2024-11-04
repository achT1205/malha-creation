using Catalog.Application.Occasions.Commands.CreateOccasion;
using Catalog.Application.Occasions.Queries;

namespace Catalog.API.Endpoints;


public static class OccasionEndpoints
{

    public record CreateOccasionRequest(
    string Name
        );
    public record CreateOccasionResponse(Guid Id);

    public static void MapOccasionEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/occasions", async (CreateOccasionRequest request, ISender sender) =>
        {
            // Adapter la requête en commande
            var command = request.Adapt<CreateOccasionCommand>();

            // Envoyer la commande via MediatR
            var result = await sender.Send(command);

            // Adapter le résultat en réponse
            var response = result.Adapt<CreateOccasionResponse>();

            // Retourner la réponse avec un statut 201 Created
            return Results.Created($"/api/occasions/{result.Id}", response);
        })
        .WithName("CreateOccasion")
        .Produces<CreateOccasionResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Occasion")
        .WithDescription("Create a new Occasion.");

        app.MapGet("/api/Occasions", async (ISender sender) =>
        {
            var query = new GetOccasionsQuery();
            var result = await sender.Send(query);
            return Results.Ok(result);
        })
       .WithName("GetOccasions")
       .Produces<List<OccasionDto>>(StatusCodes.Status200OK)
       .ProducesProblem(StatusCodes.Status400BadRequest)
       .WithSummary("Get Occasions")
       .WithDescription("Retrieve a list of all available Occasions.");
    }

}