namespace Catalog.API.Products.Commands.UpdateProduct;
public record UpdateProductRequest(Product Product);
public record UpdateProductResponse(bool IsSuccess);
public class UpdateOccasionEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
        {

            var command = request.Adapt<UpdateProductCommand>();
            var result = await sender.Send(command);

            var response = result.Adapt<UpdateProductResponse>();
            return Results.Ok(response);
        })
         .WithName("UpdateProduct")
         .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Update Product")
         .WithDescription("Update Product");
    }
}