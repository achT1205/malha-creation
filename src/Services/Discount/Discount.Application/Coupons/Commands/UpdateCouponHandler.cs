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

        if (!coupon.ProductIds.Any() && !coupon.AllowedCustomerIds.Any())
        {
            var service = new Stripe.CouponService();
            await service.DeleteAsync(coupon.Code.Value);

            var options = new Stripe.CouponCreateOptions();
            if (command.FlatAmount.HasValue && command.FlatAmount > 0)
                options.AmountOff = (long)(command.FlatAmount.Value * 100);

            if (command.Percentage.HasValue && command.Percentage > 0)
                options.PercentOff = (long)(command.Percentage.Value * 100);
            options.Name = command.Name;
            options.Currency = "usd";
            options.Id = command.CouponCode;

            await service.CreateAsync(options);
        }

        return new UpdateCouponResult(true);
    }
}
