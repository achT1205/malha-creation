using Catalog.Application.Occasions.Commands.DeleteOccasion;
using Catalog.Application.Occasions.Commands.CreateOccasion;
using Catalog.Application.Occasions.Queries;

namespace Catalog.API.Endpoints;
public static class OccasionEndpoints
{

    public record CreateOccasionRequest(string Name, string Description);
    public record CreateOccasionResponse(Guid Id);

    public record DeleteOccasionResponse(bool IsSuccess);

    public static void MapOccasionEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/occasions", async (CreateOccasionRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateOccasionCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateOccasionResponse>();
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


        app.MapDelete("/api/occasions/{id}", async (Guid id, ISender sender) =>
        {
            var query = new DeleteOccasionCommand(id);
            var result = await sender.Send(query);
            var response = result.Adapt<DeleteOccasionResponse>();
            return Results.Ok(response);
        })
        .WithName("DeleteOccasionResponse")
        .Produces<DeleteOccasionResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Occasion")
        .WithDescription("Delete Occasion.");
    }

}