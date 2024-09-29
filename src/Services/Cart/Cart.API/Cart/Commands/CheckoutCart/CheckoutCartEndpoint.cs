﻿namespace Cart.API.Cart.Commands.CheckoutCart;

public record CheckoutCartRequest(CartCheckoutDto CartCheckoutDto);
public record CheckoutCartResponse(bool IsSuccess);

public class CheckoutCartEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/cart/checkout", async (CheckoutCartRequest request, ISender sender) =>
        {
            var command = request.Adapt<CheckoutCartCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CheckoutCartResponse>();

            return Results.Ok(response);
        })
        .WithName("CheckoutCart")
        .Produces<CheckoutCartResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Checkout Cart")
        .WithDescription("Checkout Cart");
    }
}