using Weasel.Core;
using BuildingBlocks.Messaging.MassTransit;
using Catalog.API.Services.Interfaces;
using Catalog.API.Services;
using Catalog.API.Configs;
using BuildingBlocks.Middlewares;

var builder = WebApplication.CreateBuilder(args);

//Add service to the dependence injection container (DI)

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();
builder.Services.AddMarten((options) =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);

    if (builder.Environment.IsDevelopment())
    {
        options.AutoCreateSchemaObjects = AutoCreate.All;
    }
}).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//Async Communication Services
builder.Services.AddMessageBroker(builder.Configuration);

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

// Bind the ExternalApiSettings from appsettings.json
builder.Services.Configure<ExternalApiSettings>(builder.Configuration.GetSection("ExternalApiSettings"));

// Register HttpClient
builder.Services.AddHttpClient();

// Register the external API service
builder.Services.AddScoped<ISctockApiService, SctockApiService>();

var app = builder.Build();

//Configure the HTTP request pipeline

app.UseMiddleware<RetrictAccessMiddleware>();

app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
