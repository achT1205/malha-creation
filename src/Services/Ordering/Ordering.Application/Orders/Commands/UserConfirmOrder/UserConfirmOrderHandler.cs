namespace Ordering.Application.Orders.Commands.ConfirmOrder;

public record UserConfirmOrderCommand(Guid Id) : ICommand<UserConfirmOrderResult>;
public record UserConfirmOrderResult(bool IsSuccess);
public class UserConfirmOrderCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<UserConfirmOrderCommand, UserConfirmOrderResult>
{
    public async Task<UserConfirmOrderResult> Handle(UserConfirmOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
           .SingleOrDefaultAsync(t => t.Id == OrderId.Of(command.Id), cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.Id);
        }

        order.ConfirmOrder();

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UserConfirmOrderResult(true);

    }
}
