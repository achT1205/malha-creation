namespace Inventory.API.Stocks.Commands.UpdateStock;
public record UpdateStockRequest(Stock Stock);
public record UpdateStockResponse(bool IsSuccess);
public class UpdateStockEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/stocks", async (UpdateStockRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateStockCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateStockResponse>();
            return Results.Ok(response);    
        })
        .WithName("UpdateStock")
        .Produces<UpdateStockResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("update Stock")
        .WithDescription("Update an existing Stock based on its type (clothing or accessory).");
    }
}