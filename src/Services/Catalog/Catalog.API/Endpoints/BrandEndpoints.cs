﻿using Catalog.Application.Brands.Commands.CreateBrand;
using Catalog.Application.Brands.Queries;


namespace Catalog.API.Endpoints;


public static class BrandEndpoints
{

    public record CreateBrandRequest(
    string Name
        );
    public record CreateBrandResponse(Guid Id);

    public static void MapBrandEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/brands", async (CreateBrandRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateBrandCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateBrandResponse>();
            return Results.Created($"/api/brands/{result.Id}", response);
        })
        .WithName("CreateBrand")
        .Produces<CreateBrandResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Brand")
        .WithDescription("Create a new Brand.");

        app.MapGet("/api/brands", async (ISender sender) =>
        {
            var query = new GetBrandsQuery();
            var result = await sender.Send(query);
            return Results.Ok(result);
        })
       .WithName("Getbrands")
       .Produces<List<BrandDto>>(StatusCodes.Status200OK)
       .ProducesProblem(StatusCodes.Status400BadRequest)
       .WithSummary("Get brands")
       .WithDescription("Retrieve a list of all available brands.");
    }

}