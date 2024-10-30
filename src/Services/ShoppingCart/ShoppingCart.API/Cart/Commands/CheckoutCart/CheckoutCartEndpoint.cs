namespace Cart.API.Cart.Commands.CheckoutCart;

public record CheckoutCartRequest
{

    public Guid UserId { get; set; } = default!;

    // Shipping and BillingAddress
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public string AddressLine { get; set; } = default!;
    public string Country { get; set; } = default!;
    public string State { get; set; } = default!;
    public string ZipCode { get; set; } = default!;

    // Payment
    public string CardName { get; set; } = default!;
    public string CardNumber { get; set; } = default!;
    public string Expiration { get; set; } = default!;
    public string CVV { get; set; } = default!;
    public int PaymentMethod { get; set; } = default!;
}
public record CheckoutCartResponse(bool IsSuccess);

public class CheckoutCartEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/cart/checkout", async (CheckoutCartRequest request, ISender sender) =>
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