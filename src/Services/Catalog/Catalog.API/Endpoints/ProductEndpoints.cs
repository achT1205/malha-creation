using BuildingBlocks.Enums;
using Catalog.Application.Dtos;
using Catalog.Application.Products.Commands.DeleteProduct;
using Catalog.Application.Products.Commands.UpdateProduct;
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
    ImageDto CoverImage,
    Guid ProductTypeId,
    Guid MaterialId,
    Guid CollectionId,
    List<Guid> OccasionIds,
    List<Guid> CategoryIds,
    List<ColorVariantDto> ColorVariants
);

    public record UpdateProductRequest(
        Guid Id,
        string Name,
        string UrlFriendlyName,
        string Description,
        bool IsHandmade,
        ImageDto CoverImage,
        Guid ProductTypeId,
        ProductTypeEnum ProductType,
        Guid MaterialId,
        Guid CollectionId,
        List<Guid> OccasionIds,
        List<Guid> CategoryIds,
        List<ColorVariantDto> ColorVariants,
        RemovedItems? RemovedItems
       );
    public record UpdateProductResponse(bool IsSuccess);
    public record DeleteProductResponse(bool IsSuccess);
    public record CreateProductResponse(Guid Id);
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
        .WithDescription("Create a new product.");


        app.MapPut("/api/products", async (UpdateProductRequest request, ISender sender) =>
        {
            // Adapter la requête en commande
            var command = request.Adapt<UpdateProductCommand>();

            // Envoyer la commande via MediatR
            var result = await sender.Send(command);

            // Adapter le résultat en réponse
            var response = result.Adapt<UpdateProductResult>();

            // Retourner la réponse avec un statut 201 Created
            return Results.Ok(response);
        })
        .WithName("UpdateProduct")
        .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Product")
        .WithDescription("Update an existing product.");


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

        app.MapDelete("/api/products/{id}", async (Guid id, ISender sender) =>
        {
            // Créer la requête pour obtenir le produit par slug
            var query = new DeleteProductCommand(id);

            // Envoyer la requête via MediatR
            var result = await sender.Send(query);

            var response = result.Adapt<DeleteProductResponse>();
            return Results.Ok(response);
        })
        .WithName("DeleteProductResponse")
        .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Product")
        .WithDescription("Delete Product.");


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
