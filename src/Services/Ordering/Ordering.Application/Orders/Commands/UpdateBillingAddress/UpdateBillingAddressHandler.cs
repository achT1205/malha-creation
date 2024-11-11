namespace Ordering.Application.Orders.Commands.UpdateBillingAddress;
public record UpdateBillingAddressCommand(Guid Id, AddressDto Address) : ICommand<UpdateBillingAddressResult>;
public record UpdateBillingAddressResult(bool IsSuccess);
public class UpdateBillingAddressCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateBillingAddressCommand, UpdateBillingAddressResult>
{
    public async Task<UpdateBillingAddressResult> Handle(UpdateBillingAddressCommand command, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
           .SingleOrDefaultAsync(t => t.Id == OrderId.Of(command.Id), cancellationToken);
        if (order is null)
        {
            throw new OrderNotFoundException(command.Id);
        }
        var billingAddress = Address.Of(command.Address.FirstName, command.Address.LastName, command.Address.EmailAddress, command.Address.AddressLine, command.Address.Country, command.Address.City, command.Address.ZipCode);
        
        order.UpdateBillingAddress(billingAddress);
        
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateBillingAddressResult(true);

    }
}