namespace Cart.API.Cart.Commands.DeleteCart;
public record DeleteCartResponse(bool IsSuccess);
public class DeleteCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/carts/{userId}", async (Guid userId, ISender sender) =>
        {
            var result = await sender.Send(new DeleteCartCommand(userId));

            var response = result.Adapt<DeleteCartResponse>();

            return Results.Ok(response);
        })
         .WithName("DeleteCart")
         .Produces<DeleteCartResponse>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .ProducesProblem(StatusCodes.Status404NotFound)
         .WithSummary("Delete Cart")
         .WithDescription("Delete Cart");
    }
}