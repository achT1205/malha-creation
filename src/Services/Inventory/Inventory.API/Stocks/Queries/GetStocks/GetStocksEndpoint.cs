
namespace Inventory.API.Stocks.Queries.GetStocks;
public record GetStocksQueryResponse(IEnumerable<Stock> Stocks);
public class GetStockByProductIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/stocks", async (ISender sender) =>
        {
            var result = await sender.Send(new GetStocksQuery());

            var response = result.Adapt<GetStocksQueryResponse>();
            return Results.Ok(response);
        })
            .WithName("GetStocks")
            .Produces<GetStocksQueryResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Stocks")
            .WithDescription("Get Stocks");
    }
}