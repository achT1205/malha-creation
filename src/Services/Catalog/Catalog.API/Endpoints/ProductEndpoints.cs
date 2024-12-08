using BuildingBlocks.Pagination;
using Catalog.Application.Products.Commands.DeleteProduct;
using Catalog.Application.Products.Commands.UpdateProduct;
using Catalog.Application.Products.Queries.GetProductById;
using Catalog.Application.Products.Queries.GetProductBySlug;
using Catalog.Application.Products.Queries.GetProducts;
using Catalog.Domain.Enums;

namespace Catalog.API.Endpoints;

public static class ProductEndpoints
{
    public record CreateProductRequest(
    string Name,
    string UrlFriendlyName,
    string Description,
    string ShippingAndReturns,
    string? Code,
    bool IsHandmade,
    bool OnReorder,
    ImageDto CoverImage,
    ProductType ProductType,
    Guid MaterialId,
    Guid BrandId,
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
    ProductStatus Status,
    ProductType ProductType,
    string ShippingAndReturns,
    string? Code,
    bool IsHandmade,
    ImageDto CoverImage,
    Guid MaterialId,
    Guid BrandId,
    Guid CollectionId,
    List<Guid> OccasionIds,
    List<Guid> CategoryIds,
    List<ColorVariantDto> ColorVariants
   );

    public record UpdateProductResponse(bool IsSuccess);
    public record DeleteProductResponse(bool IsSuccess);
    public record CreateProductResponse(Guid Id);

    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/products", async (CreateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateProductResponse>();
            return Results.Created($"/api/products/{command.UrlFriendlyName}", response);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create a new product.");

        app.MapPut("/api/products/{id}", async (Guid Id, UpdateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateProductCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateProductResponse>();
            return Results.Ok(response);
        })
        .WithName("Update Product")
        .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Product.")
        .WithDescription("Update Product.");

        app.MapDelete("/api/products/{id}", async (Guid id, ISender sender) =>
        {
            var query = new DeleteProductCommand(id);
            var result = await sender.Send(query);
            var response = result.Adapt<DeleteProductResponse>();
            return Results.Ok(response);
        })
        .WithName("DeleteProductResponse")
        .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Product")
        .WithDescription("Delete Product.");

        app.MapGet("/api/products/by-slug/{slug:regex(^[a-zA-Z0-9_-]+$)}", async (string slug, ISender sender) =>
        {
            var query = new GetProductBySlugQuery(slug);
            var result = await sender.Send(query);
            return Results.Ok(result);
        })
        .WithName("GetProductBySlug")
        .Produces<ProductDto>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Product by Slug")
        .WithDescription("Retrieve product details by slug.")
        .WithOrder(1);

        app.MapGet("/api/products/by-id/{id:guid}", async (Guid id, ISender sender) =>
        {
            var query = new GetProductByIdQuery(id);
            var result = await sender.Send(query);
            return Results.Ok(result);
        })
       .WithName("GetProductById")
       .Produces<ProductDto>(StatusCodes.Status200OK)
       .ProducesProblem(StatusCodes.Status404NotFound)
       .WithSummary("Get Product by Id")
       .WithDescription("Retrieve product details by Id.")
       .WithOrder(2);

        app.MapGet("/api/products", async ([AsParameters] PaginationRequest request, ISender sender) =>
        {
            var query = new GetProductsQuery(request);
            var result = await sender.Send(query);
            return Results.Ok(result);
        })
        .WithName("GetProducts")
        .Produces<GetProductsQueryResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Retrieve a list of all available products.");
    }
}
