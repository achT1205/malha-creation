namespace Catalog.API.Categories.Queries.GetCategoryById;

public record GetCategoryByIdRequest(Guid Id);
public record GetCategoryByIdResponse(Category Category);
public class GetMaterialByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/categories/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetCategoryByIdQuery(id));

            var response = result.Adapt<GetCategoryByIdResponse>();
            return Results.Ok(response);
        })
         .WithName("GetCategoryById")
         .Produces<GetCategoryByIdResponse>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Get Category by id")
         .WithDescription("Get Category by id");
    }
}