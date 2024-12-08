namespace Ordering.Application.Orders.Commands.RejectedOrder;
public record OrderItemStockRejectCommand(OrderStockRejectedDto Order) : ICommand<OrderItemStockRejectResult>;
public record OrderItemStockRejectResult(bool IsSuccess);
public class OrderItemStockRejectCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<OrderItemStockRejectCommand, OrderItemStockRejectResult>
{
    public async Task<OrderItemStockRejectResult> Handle(OrderItemStockRejectCommand command, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
           .SingleOrDefaultAsync(t => t.Id == OrderId.Of(command.Order.OrderId), cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.Order.OrderId);
        }

        order.MarkAsFailed();

        await dbContext.SaveChangesAsync(cancellationToken);

        return new OrderItemStockRejectResult(true);

    }
}