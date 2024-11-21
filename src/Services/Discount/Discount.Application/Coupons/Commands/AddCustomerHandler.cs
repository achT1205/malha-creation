namespace Discount.Application.Coupons.Commands;

public record AddCustomerCommand(Guid CouponId, Guid CustomerId) : ICommand<AddCustomerResult>;
public record AddCustomerResult(bool IsSuccess);

public class AddCustomerCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<AddCustomerCommand, AddCustomerResult>
{
    public async Task<AddCustomerResult> Handle(AddCustomerCommand command, CancellationToken cancellationToken)
    {
        var coupon = await dbContext.Coupons
            .Include(c => c.AllowedCustomerIds)
        .FirstOrDefaultAsync(c => c.Id.Value == command.CouponId, cancellationToken);

        if (coupon == null)
            throw new NotFoundException($"Coupon with ID {command.CouponId} not found.");

        coupon.AddSpecificCustomer(CustomerId.Of(command.CustomerId));

        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new AddCustomerResult(true);

    }
}
