namespace Ordering.Application.Orders.Commands.UpdateShippingAddress;
public record UpdateShippingAddressCommand(Guid Id, AddressDto Address) : ICommand<UpdateShippingAddressResult>;
public record UpdateShippingAddressResult(bool IsSuccess);
public class UpdateShippingAddressCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateShippingAddressCommand, UpdateShippingAddressResult>
{
    public async Task<UpdateShippingAddressResult> Handle(UpdateShippingAddressCommand command, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
           .SingleOrDefaultAsync(t => t.Id == OrderId.Of(command.Id), cancellationToken);
        if (order is null)
        {
            throw new OrderNotFoundException(command.Id);
        }
        var address = Address.Of(command.Address.FirstName, command.Address.LastName, command.Address.EmailAddress, command.Address.AddressLine, command.Address.Country, command.Address.City, command.Address.ZipCode);
        
        order.UpdateShippingAddress(address);
        
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateShippingAddressResult(true);

    }
}