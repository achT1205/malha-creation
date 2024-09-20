namespace Catalog.API.Products.Queries.GeProductBySlug;
public record GeProductBySlugRequest(string Slug);
public record GeProductBySlugResponse(object Product);
public class GetMaterialByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{slug:regex(^[a-zA-Z0-9 \\-]+$)}", async (string slug, ISender sender) =>
        {
            var result = await sender.Send(new GeProductBySlugQuery(slug));
            var response = result.Adapt<GeProductBySlugResponse>();
            return Results.Ok(response);
        })
         .WithOrder(2)
         .WithName("GeProductBySlug")
         .Produces<GeProductBySlugResponse>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Get Product by slug")
         .WithDescription("Get Product by slug");
    }
}