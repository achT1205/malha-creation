
namespace Inventory.API.Stocks.Queries.GetStockByProductId;
public record GetStockByProductIdQueryResponse(Stock Stock);
public class GetStockEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/stocks/{id:Guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetStockByProductIdQuery(id));

            var response = result.Adapt<GetStockByProductIdQueryResponse>();
            return Results.Ok(response);
        }).WithOrder(1)
          .WithName("GetStockByProductId")
          .Produces<GetStockByProductIdQueryResponse>(StatusCodes.Status201Created)
          .ProducesProblem(StatusCodes.Status400BadRequest)
          .WithSummary("Get Stock By ProductId")
          .WithDescription("Get Stock By ProductId");
    }
}