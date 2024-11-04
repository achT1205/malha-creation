using Catalog.Application.Categories.Commands.CreateCategory;
using Catalog.Application.Categories.Queries;


namespace Catalog.API.Endpoints;


public static class CategoryEndpoints
{

    public record CreateCategoryRequest(
    string Name
        );
    public record CreateCategoryResponse(Guid Id);

    public static void MapCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/categories", async (CreateCategoryRequest request, ISender sender) =>
        {
            // Adapter la requête en commande
            var command = request.Adapt<CreateCategoryCommand>();

            // Envoyer la commande via MediatR
            var result = await sender.Send(command);

            // Adapter le résultat en réponse
            var response = result.Adapt<CreateCategoryResponse>();

            // Retourner la réponse avec un statut 201 Created
            return Results.Created($"/api/categories/{result.Id}", response);
        })
        .WithName("CreateCategory")
        .Produces<CreateCategoryResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Category")
        .WithDescription("Create a new Category.");

        app.MapGet("/api/categories", async (ISender sender) =>
        {
            var query = new GetCategoriesQuery();
            var result = await sender.Send(query);
            return Results.Ok(result);
        })
       .WithName("Getcategories")
       .Produces<List<CategoryDto>>(StatusCodes.Status200OK)
       .ProducesProblem(StatusCodes.Status400BadRequest)
       .WithSummary("Get categories")
       .WithDescription("Retrieve a list of all available categories.");
    }

}