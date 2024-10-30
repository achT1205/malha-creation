
namespace Cart.API.Cart.Commands.Discount;
public record ApplyCartDiscountRequest(ApplyCartDiscountDto DiscountDto);
public record ApplyCartDiscountRsponse(Guid UserId);
public class ApplyCartDiscountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/carts/appycoupon", async (ApplyCartDiscountRequest request, ISender sender) =>
        {
            var command = request.Adapt<ApplyCartDiscountCommand>();
            var result = await sender.Send(command);

            var response = result.Adapt<ApplyCartDiscountRsponse>();
            return Results.Ok(response);
        })
         .WithName("applycoupon")
         .Produces<ApplyCartDiscountRsponse>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("apply coupon")
         .WithDescription("appy coupon");
    }
}