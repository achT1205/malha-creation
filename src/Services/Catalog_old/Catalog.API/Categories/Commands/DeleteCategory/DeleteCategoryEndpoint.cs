namespace Catalog.API.Categories.Commands.DeleteCategory;
public record DeleteCategoryResponse(bool IsSuccess);
public class DeleteOccasionEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/categories/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteCategoryCommand(id));

            var response = result.Adapt<DeleteCategoryResponse>();
            return Results.Ok(response);
        })
        .WithName("DeleteCategory")
        .Produces<DeleteCategoryResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("delete Category")
        .WithDescription("delete Category");
    }
}