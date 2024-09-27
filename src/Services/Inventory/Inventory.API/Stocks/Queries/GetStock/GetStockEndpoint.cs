
namespace Inventory.API.Stocks.Queries.GetStock;
public record GetStockQueryResponse(Stock Stock);
public class GetStockEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/stocks/{id:Guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetStockQuery(id));

            var response = result.Adapt<GetStockQueryResponse>();
            return Results.Ok(response);
        })
            .WithOrder(2)
            .WithName("GetStock")
            .Produces<GetStockQueryResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Stock By StockId")
            .WithDescription("Get Stock By StockId");
    }
}