using BuildingBlocks.Pagination;
using Catalog.Application.Products.Commands.AddColorVariant;
using Catalog.Application.Products.Commands.AddColorVariantStock;
using Catalog.Application.Products.Commands.AddSizeVariant;
using Catalog.Application.Products.Commands.AddSizeVariantStock;
using Catalog.Application.Products.Commands.DeleteProduct;
using Catalog.Application.Products.Commands.RemoveColorVariant;
using Catalog.Application.Products.Commands.RemoveSizeVariant;
using Catalog.Application.Products.Commands.UpdateCategories;
using Catalog.Application.Products.Commands.UpdateColorVariantPrice;
using Catalog.Application.Products.Commands.UpdateOccasions;
using Catalog.Application.Products.Commands.UpdateProductInfos;
using Catalog.Application.Products.Commands.UpdateSizeVariantPrice;
using Catalog.Application.Products.Queries.GetProductById;
using Catalog.Application.Products.Queries.GetProductBySlug;
using Catalog.Application.Products.Queries.GetProducts;
using Catalog.Domain.Enums;
using Catalog.Domain.ValueObjects;

namespace Catalog.API.Endpoints;

public static class ProductEndpoints
{
    public record CreateProductRequest(
    string Name,
    string UrlFriendlyName,
    string Description,
    bool IsHandmade,
    bool OnReorder,
    ImageDto CoverImage,
    Guid ProductTypeId,
    ProductType ProductType,
    Guid MaterialId,
    Guid BrandId,
    Guid CollectionId,
    List<Guid> OccasionIds,
    List<Guid> CategoryIds,
    List<ColorVariantDto> ColorVariants
);
    public record UpdateProductInfosRequest(
        Guid Id,
        string Name,
        string UrlFriendlyName,
        string Description,
        bool IsHandmade,
        ImageDto CoverImage,
        Guid MaterialId,
        Guid BrandId,
        Guid CollectionId
       );
    public record UpdateOccasionsRequest(Guid Id, List<Guid> OccasionIds);
    public record UpdateCategoriesRequest(Guid Id, List<Guid> CategoryIds);
    public record UpdateColorVariantPriceRequest(Guid Id, Guid ColorVariantId, decimal Price);
    public record UpdateSizeVariantPriceRequest(Guid Id, Guid ColorVariantId, Guid SizeVariantId, decimal Price);
    public record AddSizeVariantStockRequest(Guid Id, Guid ColorVariantId, Guid SizeVariantId, int Quantity);
    public record AddColorVariantStockRequest(Guid Id, Guid ColorVariantId, int Quantity);
    public record AddColorVariantRequest(
    Guid Id,
    string Color,
    List<ImageDto> Images,
    decimal? Price,
    int? Quantity,
    List<SizeVariantDto>? SizeVariants,
    int? RestockThreshold
    );
    public record AddSizeVariantRequest(
    Guid Id,
    Guid ColorVariantId,
    string Size,
    decimal Price,
    int Quantity,
    int RestockThreshold);
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

        app.MapPut("/api/products/{id}/infos", async (Guid Id, UpdateProductInfosRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateProductInfosCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateProductResponse>();
            return Results.Ok(response);
        })
        .WithName("Update Product Infos")
        .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Product infos.")
        .WithDescription("Update Product infos.");


        app.MapPut("/api/products/{id}/occasions", async (Guid Id, UpdateOccasionsRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateOccasionsCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateProductResponse>();
            return Results.Ok(response);
        })
        .WithName("Update Product Ocations")
        .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Product occasions.")
        .WithDescription("Update Product occasions.");

        app.MapPut("/api/products/{id}/categories", async (Guid Id, UpdateCategoriesRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateCategoriesCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateProductResponse>();
            return Results.Ok(response);
        })
        .WithName("Update Product Categories")
        .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Product categories.")
        .WithDescription("Update Product categories.");


        app.MapPut("/api/products/{id}/color-variants/{colorVariantId}/add-stock", async (
            Guid id,
            Guid colorVariantId,
            AddColorVariantStockRequest requst,
            ISender sender) =>
                {
                    var command = requst.Adapt<AddColorVariantStockCommand>();
                    var result = await sender.Send(command);
                    var response = result.Adapt<UpdateProductResponse>();
                    return Results.Ok(response);
                })
        .WithName("add Color variant stock")
        .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Product: Add ColorVariant Stock.")
        .WithDescription("Update Product: Add ColorVariant Stock.");


        app.MapPut("/api/products/{id}/color-variants/{colorVariantId}/size-variants/{sizeVariantId}/add-stock", async (
            Guid id, 
            Guid colorVariantId, 
            Guid SizeVariantId, 
            AddSizeVariantStockRequest requst, 
            ISender sender) =>
        {
            var command = requst.Adapt<AddSizeVariantStockCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateProductResponse>();
            return Results.Ok(response);
        })
        .WithName("Add SizeVariant Stock")
        .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Product: Add SizeVariant Stock.")
        .WithDescription("Update Product: Add SizeVariant Stock.");


        app.MapPut("/api/products/{id}/color-variants/{colorVariantId}/update-price", async (Guid id, Guid colorVariantId, UpdateColorVariantPriceRequest requst, ISender sender) =>
        {
            var command = requst.Adapt<UpdateColorVariantPriceCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateProductResponse>();
            return Results.Ok(response);
        })
        .WithName("Update Product color variant price")
        .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Product: Update ColorVariant Price.")
        .WithDescription("Update Product: Update ColorVariant Price.");


        app.MapPut("/api/products/{id}/color-variants/{colorVariantId}/size-variants/{sizeVariantId}/update-price", async (Guid id, Guid colorVariantId, Guid sizeVariantId, UpdateSizeVariantPriceRequest requst, ISender sender) =>
        {
            var command = requst.Adapt<UpdateSizeVariantPriceCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateProductResponse>();
            return Results.Ok(response);
        })
        .WithName("Update Product size variant price")
        .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Product: Update Size variant Price.")
        .WithDescription("Update Product: Update Size variant Price.");

        //

        app.MapDelete("/api/products/{id}/color-variants/{colorVariantId}", async (Guid id, Guid colorVariantId, ISender sender) =>
        {
            var command = new RemoveColorVariantCommand(id, colorVariantId);
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateProductResponse>();
            return Results.Ok(response);
        })
        .WithName("Delete Product Color variant")
        .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Product: Remove colorVariant.")
        .WithDescription("Update Product: Remove colorVariant.");

        app.MapDelete("/api/products/{id}/color-variants/{colorVariantId}/size-variants/{sizeVariantId}/", async (Guid id, Guid colorVariantId, Guid sizeVariantId, ISender sender) =>
        {
            var command = new RemoveSizeVariantCommand(id, colorVariantId, sizeVariantId);
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateProductResponse>();
            return Results.Ok(response);
        })
        .WithName("Delete Product size variant")
        .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Product: Remove SizeVariant.")
        .WithDescription("Update Product: Remove SizeVariant.");

        app.MapPost("/api/products/{id}/color-variants/{colorVariantId}/size-variants/", async (Guid id, Guid colorVariantId, AddSizeVariantRequest request, ISender sender) =>
        {
            var command = request.Adapt<AddSizeVariantCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateProductResponse>();
            return Results.Ok(response);
        })
        .WithName("Add Product size variant")
        .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Product: Add Product size variant.")
        .WithDescription("Update Product: Add Product size variant.");

        app.MapPost("/api/products/{id}/color-variants", async (Guid id, AddColorVariantRequest request, ISender sender) =>
        {
            var command = request.Adapt<AddColorVariantCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateProductResponse>();
            return Results.Ok(response);
        })
        .WithName("Add Product color variant")
        .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Product: Add Product color variant.")
        .WithDescription("Update Product: Add Product color variant.");


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


        app.MapGet("/api/products", async ([AsParameters] PaginationRequest request, ISender sender) =>
        {
            var query = new GetProductsQuery(request);
            var result = await sender.Send(query);
            return Results.Ok(result);
        })
        .WithName("GetProducts")
        .Produces<List<ProductDto>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Retrieve a list of all available products.");
    }
}
