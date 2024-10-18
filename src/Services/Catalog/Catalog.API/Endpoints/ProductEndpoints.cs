using Catalog.Application.Products.Commands.AddProductVariant;
using Catalog.Application.Products.Dtos;
using Catalog.Application.Products.Queries.GetProductBySlug;
using Catalog.Application.Products.Queries.GetProducts;

namespace Catalog.API.Endpoints;

public static class ProductEndpoints
{

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

    public record AddProductVariantRequest(
    Guid ProductId,
    CreateColorVariantDto ColorVariant
        );
    public record AddProductVariantResponse(Guid Id);

    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
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

        app.MapGet("/api/products/{slug}", async (string slug, ISender sender) =>
        {
            // Créer la requête pour obtenir le produit par slug
            var query = new GetProductBySlugQuery(slug);

            // Envoyer la requête via MediatR
            var result = await sender.Send(query);

            if (result is null)
            {
                return Results.NotFound($"Product with slug '{slug}' not found.");
            }

            // Retourner la réponse avec un statut 200 OK
            return Results.Ok(result);
        })
        .WithName("GetProductBySlug")
        .Produces<ProductDto>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Product by Slug")
        .WithDescription("Retrieve product details by slug.");

        // Endpoint pour obtenir tous les produits
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


