namespace Ordering.Application.Orders.Commands.ConfirmOrder;

public record StartProcessingOrderCommand(Guid Id) : ICommand<StartProcessingOrderResult>;
public record StartProcessingOrderResult(bool IsSuccess);
public class StartProcessingOrderCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<StartProcessingOrderCommand, StartProcessingOrderResult>
{
    public async Task<StartProcessingOrderResult> Handle(StartProcessingOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
           .SingleOrDefaultAsync(t => t.Id == OrderId.Of(command.Id), cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.Id);
        }

        order.StartProcessing();

        await dbContext.SaveChangesAsync(cancellationToken);

        return new StartProcessingOrderResult(true);

    }
}
