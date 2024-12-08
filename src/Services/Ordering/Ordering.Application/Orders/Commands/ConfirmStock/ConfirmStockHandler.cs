namespace Ordering.Application.Orders.Commands.ConfirmOrder;

public record ConfirmStockCommand(Guid Id) : ICommand<ConfirmStockResult>;
public record ConfirmStockResult(bool IsSuccess);
public class ConfirmStockCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<ConfirmStockCommand, ConfirmStockResult>
{
    public async Task<ConfirmStockResult> Handle(ConfirmStockCommand command, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
           .SingleOrDefaultAsync(t => t.Id == OrderId.Of(command.Id), cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.Id);
        }

        order.ConfirmStock();

        await dbContext.SaveChangesAsync(cancellationToken);

        return new ConfirmStockResult(true);

    }
}
