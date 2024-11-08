using BuildingBlocks.Middlewares;
using Catalog.API;
using Catalog.API.Endpoints;
using Catalog.Application;
using Catalog.Infrastructure;
using Catalog.Infrastructure.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}
//app.UseMiddleware<RetrictAccessMiddleware>();

app.MapProductEndpoints();
app.MapCategoryEndpoints();
app.MapCollectionEndpoints();
app.MapMaterialEndpoints();
app.MapOccasionEndpoints();
app.MapProductTypeEndpoints();

app.Run();