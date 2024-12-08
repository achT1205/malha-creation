using ShoppingCart.API.Dtos;

namespace Cart.API.Cart.Commands.StoreCart;
public record StoreCartRequest
{
    public Guid UserId { get; set; } = default!;
    public List<CartItem> Items { get; set; } = new();
};
public record StoreCartResponse(Basket Basket);
public class StoreCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/carts", async (StoreCartRequest request, ISender sender) =>
        {
            var command = request.Adapt<StoreCartCommand>();
            var result = await sender.Send(command);

            var response = result.Adapt<StoreCartResponse>();
            return Results.Created($"/carts/{request.UserId}", response);
        })
         .WithName("StoreCart")
         .Produces<StoreCartResponse>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("store Cart")
         .WithDescription("store Cart");
    }
}
