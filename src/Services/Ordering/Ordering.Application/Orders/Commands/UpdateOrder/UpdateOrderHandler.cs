
namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
            .SingleOrDefaultAsync(t => t.Id.Value == command.OrderId, cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.OrderId);
        }

        UpdateOrderWithNewValues(order, command);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateOrderResult(true);
    }

    public void UpdateOrderWithNewValues(Order order, UpdateOrderCommand orderDto)
    {
        var updatedShippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);
        var updatedBillingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);
        var updatedPayment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod);

        order.Update(
            shippingAddress: updatedShippingAddress,
            billingAddress: updatedBillingAddress,
            payment: updatedPayment);
    }
}