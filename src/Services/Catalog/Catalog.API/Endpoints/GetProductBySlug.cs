
using Catalog.Application.Products.Dtos;
using Catalog.Application.Products.Queries.GetProductBySlug;

namespace Catalog.API.Endpoints;

public class GetProductBySlug : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/products/{slug}", async (string slug, ISender sender) =>
        {
            // Créer la requête pour obtenir le produit par slug
            var query = new GetProductBySlugQuery(slug);

            // Envoyer la requête via MediatR
            var result = await sender.Send(query);

            // Retourner la réponse avec un statut 200 OK
            return Results.Ok(result);
        })
        .WithName("GetProductBySlug")
        .Produces<ProductDto>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Product by Slug")
        .WithDescription("Retrieve product details by slug.");
    }
}
