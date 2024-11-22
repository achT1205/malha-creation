namespace Discount.Application.Coupons.Commands;

public record DeleteCouponCommand : ICommand<DeleteCouponResult>
{
    public Guid CouponId { get; init; }
}

public record DeleteCouponResult(bool IsSuccess);

public class DeleteCouponCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteCouponCommand, DeleteCouponResult>
{
    public async Task<DeleteCouponResult> Handle(DeleteCouponCommand command, CancellationToken cancellationToken)
    {
        var coupon = await dbContext.Coupons
            .FirstOrDefaultAsync(c => c.Id == CouponId.Of(command.CouponId), cancellationToken);

        if (coupon == null)
            throw new NotFoundException($"Coupon with ID {command.CouponId} not found.");

        coupon.Delete();
        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteCouponResult(true);
    }
}
