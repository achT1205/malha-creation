using Catalog.Application.Categories.Commands.CreateCategory;
using Catalog.Application.Categories.Queries;
using Catalog.Application.Categories.Commands.DeleteCategory;


namespace Catalog.API.Endpoints;


public static class CategoryEndpoints
{

    public record CreateCategoryRequest(
    string Name
        );
    public record CreateCategoryResponse(Guid Id);

    public record  DeleteCategoryResponse(bool IsSuccess);

    public static void MapCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/categories", async (CreateCategoryRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateCategoryCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateCategoryResponse>();
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


        app.MapDelete("/api/categories/{id}", async (Guid id, ISender sender) =>
        {
            var query = new DeleteCategoryCommand(id);
            var result = await sender.Send(query);
            var response = result.Adapt<DeleteCategoryResponse>();
            return Results.Ok(response);
        })
        .WithName("DeleteCategoryResponse")
        .Produces<DeleteCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Category")
        .WithDescription("Delete Category.");

    }

}