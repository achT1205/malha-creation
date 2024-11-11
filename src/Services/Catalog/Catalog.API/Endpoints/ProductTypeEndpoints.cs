//using Catalog.Application.ProductTypes.Commands.DeleteProductType;
//using Catalog.Application.ProductTypes.Commands.CreateProductType;
//using Catalog.Application.ProductTypes.Queries;

//namespace Catalog.API.Endpoints;


//public static class ProductTypeEndpoints
//{

//    public record CreateProductTypeRequest(string Name);
//    public record CreateProductTypeResponse(Guid Id);

//    public record DeleteProductTypeResponse(bool IsSuccess);

//    public static void MapProductTypeEndpoints(this IEndpointRouteBuilder app)
//    {
//        app.MapPost("/api/product-types", async (CreateProductTypeRequest request, ISender sender) =>
//        {
//            var command = request.Adapt<CreateProductTypeCommand>();
//            var result = await sender.Send(command);
//            var response = result.Adapt<CreateProductTypeResponse>();
//            return Results.Created($"/api/product-types/{result.Id}", response);
//        })
//        .WithName("CreateProductType")
//        .Produces<CreateProductTypeResponse>(StatusCodes.Status201Created)
//        .ProducesProblem(StatusCodes.Status400BadRequest)
//        .WithSummary("Create ProductType")
//        .WithDescription("Create a new ProductType.");

//        app.MapGet("/api/product-types", async (ISender sender) =>
//        {
//            var query = new GetProductTypesQuery();
//            var result = await sender.Send(query);
//            return Results.Ok(result);
//        })
//       .WithName("GetProductTypes")
//       .Produces<List<ProductDto>>(StatusCodes.Status200OK)
//       .ProducesProblem(StatusCodes.Status400BadRequest)
//       .WithSummary("Get Product Types")
//       .WithDescription("Retrieve a list of all available Product Types.");

//        app.MapDelete("/api/product-types/{id}", async (Guid id, ISender sender) =>
//        {
//            var query = new DeleteProductTypeCommand(id);
//            var result = await sender.Send(query);
//            var response = result.Adapt<DeleteProductTypeResponse>();
//            return Results.Ok(response);
//        })
//        .WithName("DeleteProductTypeResponse")
//        .Produces<DeleteProductTypeResponse>(StatusCodes.Status200OK)
//        .ProducesProblem(StatusCodes.Status404NotFound)
//        .WithSummary("Delete ProductType")
//        .WithDescription("Delete ProductType.");
//    }

//}