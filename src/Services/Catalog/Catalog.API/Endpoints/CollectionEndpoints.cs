using Catalog.Application.Collections.Commands.CreateCollection;
using Catalog.Application.Collections.Commands.DeleteCollection;
using Catalog.Application.Collections.Queries;

namespace Catalog.API.Endpoints;


public static class CollectionEndpoints
{
    public record CreateCollectionRequest(string Name, string Description, ImageDto CoverImage);
    public record CreateCollectionResponse(Guid Id);

    public record DeleteCollectionResponse(bool IsSuccess);

    public static void MapCollectionEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/collections", async (CreateCollectionRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateCollectionCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateCollectionResponse>();
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

        app.MapDelete("/api/collections/{id}", async (Guid id, ISender sender) =>
        {
            var query = new DeleteCollectionCommand(id);
            var result = await sender.Send(query);
            var response = result.Adapt<DeleteCollectionResponse>();
            return Results.Ok(response);
        })
        .WithName("DeleteCollectionResponse")
        .Produces<DeleteCollectionResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Collection")
        .WithDescription("Delete Collection.");
    }

}