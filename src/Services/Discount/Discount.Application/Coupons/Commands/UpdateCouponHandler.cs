namespace Discount.Application.Coupons.Commands;

public record UpdateCouponCommand : ICommand<UpdateCouponResult>
{
    public Guid CouponId { get; init; }
    public string CouponCode { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal? FlatAmount { get; init; }
    public int? Percentage { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public int? MaxUses { get; init; }
    public int? MaxUsesPerCustomer { get; init; }
    public decimal? MinimumOrderValue { get; init; }
    public bool IsFirstTimeOrderOnly { get; init; }
    public bool IsActive { get; init; }
}

public record UpdateCouponResult(bool IsSuccess);
public class UpdateCouponCommdHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateCouponCommand, UpdateCouponResult>
{
    public async Task<UpdateCouponResult> Handle(UpdateCouponCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var coupon = await dbContext.Coupons
           .FirstOrDefaultAsync(c => c.Id == CouponId.Of(command.CouponId), cancellationToken);


            if (coupon == null)
                throw new NotFoundException($"Coupon with ID {command.CouponId} not found.");

            coupon.UpdateDetails(
                CouponCode.Of(command.CouponCode),
                command.Name,
                command.Description,
                Discountable.Of(command.FlatAmount, command.Percentage),
                command.StartDate,
                command.EndDate,
                command.MaxUses,
                command.MaxUsesPerCustomer,
                command.MinimumOrderValue,
                command.IsFirstTimeOrderOnly,
                command.IsActive
            );

            dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateCouponResult(true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
