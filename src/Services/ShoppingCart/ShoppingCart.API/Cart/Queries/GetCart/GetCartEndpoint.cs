namespace Cart.API.Cart.Queries.GetCart;

public record GetCartResponse(ShoppingCart Cart);

public class GetCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/carts/{userId}", async (Guid userId, ISender sender) =>
        {
            var result = await sender.Send(new GetCartQuery(userId));

            var response = result.Adapt<GetCartResponse>();
            return Results.Ok(response);
        })
         .WithName("GetCart")
         .Produces<GetCartResponse>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Get Cart")
         .WithDescription("Get Cart");
    }
}