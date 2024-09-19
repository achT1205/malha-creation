namespace Catalog.API.Products.Commands.CreateProduct;
public record CreateProductRequest(CreateProductDto Product);
public record CreateProductResponse(Guid Id);
public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            var type = request.Product.ProductType.ToLower();
            var command = request.Adapt<CreateProductCommand>();
            //if (type == "clothing")
               
            //else if (type == "accessory")
            //    command = request.Adapt<CreateAccessoryProductCommand>();
            //else throw new BadRequestException("Invalid Clothing Product Data");
            var result = await sender.Send(command);

            var response = result.Adapt<CreateProductResponse>();
            return Results.Created($"/products/{response.Id}", response);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create a new product based on its type (clothing or accessory).");
    }
}
