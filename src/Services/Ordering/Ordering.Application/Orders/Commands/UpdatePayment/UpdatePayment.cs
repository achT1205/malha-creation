namespace Ordering.Application.Orders.Commands.UpdatePayment;
public record UpdatePaymentCommand(Guid Id, PaymentDto  Payment) : ICommand<UpdatePaymentResult>;
public record UpdatePaymentResult(bool IsSuccess);
public class UpdatePaymentCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdatePaymentCommand, UpdatePaymentResult>
{
    public async Task<UpdatePaymentResult> Handle(UpdatePaymentCommand command, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
           .SingleOrDefaultAsync(t => t.Id == OrderId.Of(command.Id), cancellationToken);
        if (order is null)
        {
            throw new OrderNotFoundException(command.Id);
        }
        var payment = Payment.Of(command.Payment.CardName, command.Payment.CardNumber, command.Payment.Expiration, command.Payment.Cvv, command.Payment.PaymentMethod);
        order.UpdatePayment(payment);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new UpdatePaymentResult(true);

    }
}