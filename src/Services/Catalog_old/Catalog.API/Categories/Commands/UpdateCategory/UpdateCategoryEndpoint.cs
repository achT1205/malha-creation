namespace Catalog.API.Categories.Commands.UpdateCategory;
public record UpdateCategoryRequest(Category Category);
public record UpdateCategoryResponse(bool IsSuccess);
public class UpdateOccasionEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/categories", async (UpdateCategoryRequest request, ISender sender) =>
        {

            var command = request.Adapt<UpdateCategoryCommand>();
            var result = await sender.Send(command);

            var response = result.Adapt<UpdateCategoryResponse>();
            return Results.Ok(response);
        })
         .WithName("UpdateCategory")
         .Produces<UpdateCategoryResponse>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Update Category")
         .WithDescription("Update Category");
    }
}