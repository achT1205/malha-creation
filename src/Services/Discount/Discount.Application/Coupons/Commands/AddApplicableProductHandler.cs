namespace Discount.Application.Coupons.Commands;

public record AddApplicableProductCommand(Guid CouponId, Guid ProductId) : ICommand<AddApplicableProductResult>;
public record AddApplicableProductResult(bool IsSuccess);

public class AddApplicableProductCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<AddApplicableProductCommand, AddApplicableProductResult>
{
    public async Task<AddApplicableProductResult> Handle(AddApplicableProductCommand command, CancellationToken cancellationToken)
    {
        var coupon = await dbContext.Coupons
            .Include(c => c.ProductIds)
            .FirstOrDefaultAsync(c => c.Id == CouponId.Of(command.CouponId), cancellationToken);

        if (coupon == null)
            throw new NotFoundException($"Coupon with ID {command.CouponId} not found.");
        coupon.AddApplicableProduct(ProductId.Of(command.ProductId));

        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new AddApplicableProductResult(true);

    }
}
