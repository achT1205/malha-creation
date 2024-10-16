
using Catalog.Application.Products.Commands.AddProductVariant;

namespace Catalog.API.Endpoints;

public record AddProductVariantRequest(
    Guid ProductId,
    CreateColorVariantDto ColorVariant
);
public record AddProductVariantResponse(Guid Id);

public class AddProductVariant : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/products/{productId}/variants", async (Guid productId, AddProductVariantRequest request, ISender sender) =>
        {
            // Créer la commande en insérant le produit ID et en adaptant la requête
            var command = new AddProductVariantCommand(productId, request.ColorVariant);

            // Envoyer la commande via MediatR
            var result = await sender.Send(command);

            // Adapter le résultat en réponse
            var response = new AddProductVariantResponse(result.Id);

            // Retourner la réponse avec un statut 201 Created
            return Results.Created($"/api/products/{productId}/variants/{result.Id}", response);
        })
        .WithName("AddProductVariant")
        .Produces<AddProductVariantResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Add Product Variant")
        .WithDescription("Add a new color variant (Accessory or Clothing) to an existing product.");
    }
}
