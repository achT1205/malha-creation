using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Application.Orders.Commands.DeleteOrder;
using Ordering.Application.Orders.Commands.UpdateOrder;
using Ordering.Application.Orders.Queries.GetOrders;
using Ordering.Application.Orders.Queries.GetOrdersByCustomer;
using Ordering.Application.Orders.Queries.GetOrdersByOrderCode;

namespace Ordering.API.Endpoints;


public static class OrderEndpoints
{
    public record CreateOrderRequest
    {
        public Guid CustomerId { get; set; }
        public required AddressDto ShippingAddress { get; set; }
        public required AddressDto BillingAddress { get; set; }
        public required PaymentDto Payment { get; set; }
        public required List<OrderItemDto> OrderItems { get; set; }
    };
    public record CreateOrderResponse(Guid Id);
    public record UpdateOrderRequest
    {
        public Guid Id { get; set; }
        public required AddressDto ShippingAddress { get; set; }
        public required AddressDto BillingAddress { get; set; }
        public required PaymentDto Payment { get; set; }
    };
    public record UpdateOrderResponse(bool IsSuccess);

    public record DeleteOrderResponse(bool IsSuccess);

    public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);

    public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

    public record GetOrdersByCodeResponse(IEnumerable<OrderDto> Orders);
    public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (CreateOrderRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateOrderCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreateOrderResponse>();

            return Results.Created($"/orders/{response.Id}", response);
        })
       .WithName("CreateOrder")
       .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
       .ProducesProblem(StatusCodes.Status400BadRequest)
       .WithSummary("Create Order")
       .WithDescription("Create Order");

        app.MapPut("/orders", async (UpdateOrderRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateOrderCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<UpdateOrderResponse>();

            return Results.Ok(response);
        })
       .WithName("UpdateOrder")
       .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
       .ProducesProblem(StatusCodes.Status400BadRequest)
       .WithSummary("Update Order")
       .WithDescription("Update Order");

        app.MapDelete("/orders/{id}", async (Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteOrderCommand(Id));

            var response = result.Adapt<DeleteOrderResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteOrder")
        .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Order")
        .WithDescription("Delete Order");

        app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersQuery(request));

            var response = result.Adapt<GetOrdersResponse>();

            return Results.Ok(response);
        })
       .WithName("GetOrders")
       .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
       .ProducesProblem(StatusCodes.Status400BadRequest)
       .ProducesProblem(StatusCodes.Status404NotFound)
       .WithSummary("Get Orders")
       .WithDescription("Get Orders");

        app.MapGet("/orders/{code}", async (string code, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersByOrderCodeQuery(code));

            var response = result.Adapt<GetOrdersByCodeResponse>();

            return Results.Ok(response);
        })
       .WithName("GetOrdersByName")
       .Produces<GetOrdersByCodeResponse>(StatusCodes.Status200OK)
       .ProducesProblem(StatusCodes.Status400BadRequest)
       .ProducesProblem(StatusCodes.Status404NotFound)
       .WithSummary("Get Orders By Name")
       .WithDescription("Get Orders By Name");

        app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersByCustomerQuery(customerId));

            var response = result.Adapt<GetOrdersByCustomerResponse>();

            return Results.Ok(response);
        })
       .WithName("GetOrdersByCustomer")
       .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
       .ProducesProblem(StatusCodes.Status400BadRequest)
       .ProducesProblem(StatusCodes.Status404NotFound)
       .WithSummary("Get Orders By Customer")
       .WithDescription("Get Orders By Customer");


    }



}