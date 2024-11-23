using BuildingBlocks.Messaging.Events;
using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Commands.CancelOrder;
using Ordering.Application.Orders.Commands.ConfirmOrder;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Application.Orders.Commands.DeleteOrder;
using Ordering.Application.Orders.Commands.ShipOrder;
using Ordering.Application.Orders.Commands.UpdateBillingAddress;
using Ordering.Application.Orders.Commands.UpdatePayment;
using Ordering.Application.Orders.Commands.UpdateShippingAddress;
using Ordering.Application.Orders.Queries.GetOrders;
using Ordering.Application.Orders.Queries.GetOrdersByCustomer;
using Ordering.Application.Orders.Queries.GetOrdersById;
using Ordering.Application.Orders.Queries.GetOrdersByOrderCode;
using Ordering.Application.Orders.Queries.GetOrdersForStockValidation;

namespace Ordering.API.Endpoints;


public static class OrderEndpoints
{
    //public record CreateOrderRequest
    //{
    //    public Guid CustomerId { get; set; }
    //    public required AddressDto ShippingAddress { get; set; }
    //    public required AddressDto BillingAddress { get; set; }
    //    public required PaymentDto Payment { get; set; }
    //    public required List<OrderItemDto> OrderItems { get; set; }
    //};
    //public record CreateOrderResponse(Guid Id);
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
    public record GetOrderForStockValidationResponse(OrderStockDto Order);
    public record GetOrderByIdResponse(OrderDto Order);
    public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPut("/api/orders/{id}/cancel", async (Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new CancelOrderCommand(Id));
            return Results.Ok(result);
        })
        .WithName("CancelOrder")
        .Produces<CancelOrderResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Cancel Order")
        .WithDescription("Cancel Order");

        app.MapPut("/api/orders/{id}/shipping-address", async (Guid Id, AddressDto address, ISender sender) =>
        {
            var result = await sender.Send(new UpdateShippingAddressCommand(Id, address));
            return Results.Ok(result);
        })
        .WithName("UpdateShippingAddress")
        .Produces<UpdateShippingAddressResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update Shipping Address for the Order")
        .WithDescription("Update Shipping Address for the Order");

        app.MapPut("/api/orders/{id}/billing-address", async (Guid Id, AddressDto address, ISender sender) =>
        {
            var result = await sender.Send(new UpdateBillingAddressCommand(Id, address));
            return Results.Ok(result);
        })
        .WithName("UpdateBillingAddress")
        .Produces<UpdateShippingAddressResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update Billing Address for the Order")
        .WithDescription("Update Billing Address for the Order");

        app.MapPut("/api/orders/{id}/payment", async (Guid Id, PaymentDto payment, ISender sender) =>
        {
            var result = await sender.Send(new UpdatePaymentCommand(Id, payment));
            return Results.Ok(result);
        })
        .WithName("UpdatePayment")
        .Produces<UpdatePaymentResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update Payment for the Order")
        .WithDescription("Update Payment for the Order");


        app.MapPut("/api/orders/{id}/ship", async (Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new ShipOrderCommad(Id));
            return Results.Ok(result);
        })
        .WithName("ShipOrder")
        .Produces<ShipOrderResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Ship Order")
        .WithDescription("Ship Order");

        app.MapPut("/api/orders/{id}/confirm-stock", async (Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new ConfirmStockCommand(Id));
            return Results.Ok(result);
        })
        .WithName("ConfirmedStock")
        .Produces<ConfirmStockResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Confirmed Stock For the Order")
        .WithDescription("Confirmed Stock For the Order");

        app.MapGet("/api/order-by-code/{code}", async (string code, ISender sender) =>
        {
            var result = await sender.Send(new GetOrderByOrderCodeQuery(code));

            var response = result.Adapt<GetOrdersByCodeResponse>();

            return Results.Ok(response);
        })
        .WithName("GetOrdersByCode")
        .Produces<GetOrdersByCodeResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders By Code")
        .WithDescription("Get Orders By Code");

        app.MapGet("/api/get-stock-order/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetOrderForStockValidationQuery(id));

            var response = result.Adapt<GetOrderForStockValidationResponse>();

            return Results.Ok(response);
        })
        .WithName("GetOrderForStockValidation")
        .Produces<GetOrderForStockValidationResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Order For Stock Validation.")
        .WithDescription("Get Order For Stock Validation.");


        app.MapGet("/api/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
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



        app.MapGet("/api/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
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

        app.MapGet("/api/orders/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersByIdQuery(id));

            var response = result.Adapt<GetOrderByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("Get Order By Id")
        .Produces<GetOrderByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Order By Id")
        .WithDescription("Get Order By Id");

        
    }
}