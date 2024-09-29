//using Discount.Grpc;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using BuildingBlocks.Messaging.MassTransit;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Application services
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddCarter();

//Data services
builder.Services.AddMarten((options) =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
    options.Schema.For<ShoppingCart>().Identity(_ => _.UserId);
}).UseLightweightSessions();

builder.Services.AddScoped<ICartRepository, CartRepository>();
//builder.Services.Decorate<ICartRepository, CachedCartRepository>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});
//Grpc Services
//builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
//{
//    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
//})
//.ConfigurePrimaryHttpMessageHandler(() =>
//{
//    var handler = new HttpClientHandler
//    {
//        ServerCertificateCustomValidationCallback =
//       HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
//    };
//    return handler;
//});

//Async Communication Services
builder.Services.AddMessageBroker(builder.Configuration);

//Cross-cutting services
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

var app = builder.Build();

//Configure the HTTP request pipeline

app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.Run();

