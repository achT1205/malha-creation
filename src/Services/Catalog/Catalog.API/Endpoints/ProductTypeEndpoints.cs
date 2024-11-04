using Catalog.Application.Dtos;
using Catalog.Application.Products.Queries.GetProducts;
using Catalog.Application.ProductTypes.Commands.CreateProductType;
using Catalog.Application.ProductTypes.Queries;

namespace Catalog.API.Endpoints;


public static class ProductTypeEndpoints
{

    public record CreateProductTypeRequest(string Name);
    public record CreateProductTypeResponse(Guid Id);

    public static void MapProductTypeEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/product-types", async (CreateProductTypeRequest request, ISender sender) =>
        {
            // Adapter la requête en commande
            var command = request.Adapt<CreateProductTypeCommand>();

            // Envoyer la commande via MediatR
            var result = await sender.Send(command);

            // Adapter le résultat en réponse
            var response = result.Adapt<CreateProductTypeResponse>();

            // Retourner la réponse avec un statut 201 Created
            return Results.Created($"/api/product-types/{result.Id}", response);
        })
        .WithName("CreateProductType")
        .Produces<CreateProductTypeResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create ProductType")
        .WithDescription("Create a new ProductType.");

        app.MapGet("/api/product-types", async (ISender sender) =>
        {
            var query = new GetProductTypesQuery();
            var result = await sender.Send(query);
            return Results.Ok(result);
        })
       .WithName("GetProductTypes")
       .Produces<List<ProductDto>>(StatusCodes.Status200OK)
       .ProducesProblem(StatusCodes.Status400BadRequest)
       .WithSummary("Get Product Types")
       .WithDescription("Retrieve a list of all available Product Types.");
    }

}