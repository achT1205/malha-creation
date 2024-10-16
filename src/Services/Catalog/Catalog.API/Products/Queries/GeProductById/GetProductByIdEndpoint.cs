﻿namespace Catalog.API.Products.Queries.GetProductById;
public record GetProductByIdRequest(Guid Id);
public record GetProductByIdResponse(object Product);
public class GetMaterialByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:Guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(id));
            var response = result.Adapt<GetProductByIdResponse>();
            return Results.Ok(response);
        })
         .WithOrder(1)
         .WithName("GetProductById")
         .Produces<GetProductByIdResponse>(StatusCodes.Status201Created)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Get Product by id")
         .WithDescription("Get Product by id");
    }
}