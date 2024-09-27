namespace Inventory.API.Stocks.Commands.CreateStock;
public record CreateStockRequest(StockDto Stock);
public record CreateStockResponse(Guid Id);
public class UpdateStockEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/stocks", async (CreateStockRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateStockCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateStockResponse>();
            return Results.Created($"/stocks/{response.Id}", response);
        })
        .WithName("CreateStock")
        .Produces<CreateStockResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Stock")
        .WithDescription("Create a new Stock base on an existing product.");
    }
}