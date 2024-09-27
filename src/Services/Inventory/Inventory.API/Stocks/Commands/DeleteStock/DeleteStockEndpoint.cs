
namespace Inventory.API.Stocks.Commands.DeleteStock;
public record DeleteStockResponse(bool IsSuccess);
public class DeleteStockEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/stocks/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteStockCommand(id));
            var response = result.Adapt<DeleteStockResponse>();
            return Results.Ok(response);
        })
        .WithName("DeleteStock")
        .Produces<DeleteStockResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("delete Stock")
        .WithDescription("delete Stock");
    }
}