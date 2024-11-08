namespace Ordering.Application.Orders.Commands.CreateOrder;
public record CreateOrderCommand : ICommand<CreateOrderResult>
{
    public required Guid CustomerId { get; set; }
    public required AddressDto ShippingAddress { get; set; }
    public required AddressDto BillingAddress { get; set; }
    public required PaymentDto Payment { get; set; }
    public required List<OrderItemDto> OrderItems { get; set; }
}

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotNull().WithMessage("CustomerId is required");
        RuleFor(x => x.OrderItems).NotEmpty().WithMessage("OrderItems should not be empty");
        RuleFor(x => x.BillingAddress).NotEmpty().WithMessage("BillingAddress is required");
        RuleFor(x => x.ShippingAddress).NotEmpty().WithMessage("ShippingAddress is required");
        RuleFor(x => x.Payment).NotEmpty().WithMessage("Payment is required");
    }
}

public class CreateOrderCommandHandler(
    ILogger<AutoCreateOrderCommandHandler> _logger,
    IApplicationDbContext _context, 
    IProductService productService,
    IPublishEndpoint publishEndpoint)
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        // Add Integration event to clean the basket
        var orderStartedEvent = new OrderStartedEvent(command.CustomerId);
        await publishEndpoint.Publish(orderStartedEvent);

        var order = await CreateNewOrder(command);
        order.SubmitForProcessing();
        try
        {
            _logger.LogInformation("Creating Order - Order: {@Order}", order);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateOrderResult(order.Id.Value);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private async Task<Order> CreateNewOrder(CreateOrderCommand orderDto)
    {
        var shippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);
        var billingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);
        var orderId = Guid.NewGuid();
        var newOrder = Order.Create(
                id: OrderId.Of(orderId),
                customerId: CustomerId.Of(orderDto.CustomerId),
                OrderCode: OrderCode.Of(OrderCodeGenerator.GenerateOrderCode(orderDto.CustomerId)),
                shippingAddress: shippingAddress,
                billingAddress: billingAddress,
                payment: Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod)
                );

        foreach (var orderItemDto in orderDto.OrderItems)
        {
            var product = await productService.GetProductByIdAsync(orderItemDto.ProductId);
            if (product == null)
            {
                newOrder.ClearDomainEvents();
                throw new ProductNotFoundException(orderItemDto.ProductId);
            }
            decimal price = 0;
            ColorVariant variant = new();
            if (product?.ProductType == ProductType.Clothing.ToString())
            {
                variant = product.ColorVariants.FirstOrDefault(x => x.Color == orderItemDto.Color);
                var size = variant?.SizeVariants?.FirstOrDefault(x => x.Size == orderItemDto.Size);
                if (size != null) price = size.Price;
            }
            else
            {
                variant = product.ColorVariants.FirstOrDefault(x => x.Color == orderItemDto.Color);
                if (variant != null) price = variant.Price.Amount;
            }
            newOrder.Add(OrderId.Of(orderId), ProductId.Of(orderItemDto.ProductId), orderItemDto.Quantity, price, orderItemDto.Color, orderItemDto.Size, product.Name, variant.Slug);
        }
        return newOrder;
    }
}