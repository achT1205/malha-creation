using Catalog.Application.Collections.Commands.CreateCollection;
using Catalog.Application.Collections.Queries;

namespace Catalog.API.Endpoints;


public static class CollectionEndpoints
{

    public record CreateCollectionRequest(
    string Name,
    string ImageSrc,
    string AltText
        );
    public record CreateCollectionResponse(Guid Id);

    public static void MapCollectionEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/collections", async (CreateCollectionRequest request, ISender sender) =>
        {
            // Adapter la requête en commande
            var command = request.Adapt<CreateCollectionCommand>();

            // Envoyer la commande via MediatR
            var result = await sender.Send(command);

            // Adapter le résultat en réponse
            var response = result.Adapt<CreateCollectionResponse>();

            // Retourner la réponse avec un statut 201 Created
            return Results.Created($"/api/collections/{result.Id}", response);
        })
        .WithName("CreateCollection")
        .Produces<CreateCollectionResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Collection")
        .WithDescription("Create a new Collection.");

        app.MapGet("/api/collections", async (ISender sender) =>
        {
            var query = new GetCollectionsQuery();
            var result = await sender.Send(query);
            return Results.Ok(result);
        })
       .WithName("GetCollections")
       .Produces<List<CollectionDto>>(StatusCodes.Status200OK)
       .ProducesProblem(StatusCodes.Status400BadRequest)
       .WithSummary("Get Collections")
       .WithDescription("Retrieve a list of all available Collections.");
    }

}