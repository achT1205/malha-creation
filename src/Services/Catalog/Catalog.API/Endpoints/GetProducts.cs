
using Catalog.Application.Products.Dtos;
using Catalog.Application.Products.Queries.GetProducts;

namespace Catalog.API.Endpoints;

public class GetProducts : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/products", async (ISender sender) =>
        {
            // Créer la requête pour obtenir tous les produits
            var query = new GetProductsQuery();

            // Envoyer la requête via MediatR
            var result = await sender.Send(query);

            // Retourner la réponse avec un statut 200 OK
            return Results.Ok(result);
        })
        .WithName("GetProducts")
        .Produces<List<ProductDto>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Retrieve a list of all available products.");
    }
}
