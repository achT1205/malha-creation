namespace Inventory.API.Stocks.Commands.UpdateStock;
public record UpdateStockRequest
{
    public Guid Id { get; set; } = Guid.NewGuid();  // Identifiant unique pour l'entrée de stock
    public string ProductType { get; set; } = default!;// Type de produit (Clothing, Accessory)
    public List<ColorVariant> ColorVariants { get; set; } = new();  // Redéfinit les variantes pour avoir des prix directs

}
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