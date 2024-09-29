namespace Cart.API.Cart.Commands.StoreCart;
public record StoreCartRequest(ShoppingCart Cart);
public record StoreCartResponse(Guid UserId);
public class StoreCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/carts", async (StoreCartRequest request, ISender sender) =>
        {
            var command = request.Adapt<StoreCartCommand>();
            var result = await sender.Send(command);

            var response = result.Adapt<StoreCartResponse>();
            return Results.Created($"/cartss/{response.UserId}", response);
        })
         .WithName("StoreCart")
         .Produces<StoreCartResponse>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("store Cart")
         .WithDescription("store Cart");
    }
}
