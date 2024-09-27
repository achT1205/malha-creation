
namespace Inventory.API.Stocks.Commands.DeleteStockByProductId;
public record DeleteStockByProductIdCommand(Guid ProductId) : ICommand<DeleteStockByProductIdResult>;
public record DeleteStockByProductIdResult(bool IsSuccess);

public class DeleteStockByProductIdCommandValidator : AbstractValidator<DeleteStockByProductIdCommand>
{
    public DeleteStockByProductIdCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required");
    }
}
public class DeleteStockByProductIdCommandHandler(IDocumentSession session) : ICommandHandler<DeleteStockByProductIdCommand, DeleteStockByProductIdResult>
{
    public async Task<DeleteStockByProductIdResult> Handle(DeleteStockByProductIdCommand command, CancellationToken cancellationToken)
    {
        var stock = await session.Query<Stock>()
        .Where(_ => _.ProductId == command.ProductId).FirstOrDefaultAsync();
        if (stock == null)
        {
            throw new StockNotFoundException($"Stock with the ProductId {command.ProductId} is not found");
        }

        session.Delete<Stock>(stock.Id);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteStockByProductIdResult(true);
    }
}