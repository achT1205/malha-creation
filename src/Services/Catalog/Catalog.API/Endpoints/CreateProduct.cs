namespace Catalog.API.Endpoints;

public record CreateProductRequest(
    string Name,
    string UrlFriendlyName,
    string Description,
    bool IsHandmade,
    Guid ProductTypeId,
    Guid MaterialId,
    Guid CollectionId,
    Guid CoverImageId,
    List<Guid> CategoryIds,
    List<Guid> OccasionIds
);
public record CreateProductResponse(Guid Id);


public class CreateProduct : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/products", async (CreateProductRequest request, ISender sender) =>
        {
            // Adapter la requête en commande
            var command = request.Adapt<CreateProductCommand>();

            // Envoyer la commande via MediatR
            var result = await sender.Send(command);

            // Adapter le résultat en réponse
            var response = result.Adapt<CreateProductResponse>();

            // Retourner la réponse avec un statut 201 Created
            return Results.Created($"/api/products/{command.UrlFriendlyName}", response);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create a new product with the basic details like categories, occasions, and other core properties without variants.");
    }
}
