using Ordering.Application.Orders.Queries.GetOrdersByOrderCode;
using Ordering.Domain.Orders.Models;

namespace Ordering.API.Endpoints;

public record GetOrdersByNameResponse(IEnumerable<Order> Orders);

public class GetOrdersByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{orderName}", async (string code, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersByOrderCodeQuery(code));

            var response = result.Adapt<GetOrdersByNameResponse>();

            return Results.Ok(response);
        })
        .WithName("GetOrdersByName")
        .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders By Name")
        .WithDescription("Get Orders By Name");
    }
}