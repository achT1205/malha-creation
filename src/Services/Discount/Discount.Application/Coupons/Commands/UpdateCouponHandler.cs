namespace Discount.Application.Coupons.Commands;

public record UpdateCouponCommand : ICommand<UpdateCouponResult>
{
    public Guid CouponId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public Discountable Discountable { get; init; } = default!;
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public int? MaxUses { get; init; }
    public int? MaxUsesPerCustomer { get; init; }
    public decimal? MinimumOrderValue { get; init; }
    public bool IsFirstTimeOrderOnly { get; init; }
}

public record UpdateCouponResult(bool IsSuccess);
public class UpdateCouponCommdHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateCouponCommand, UpdateCouponResult>
{
    public async Task<UpdateCouponResult> Handle(UpdateCouponCommand command, CancellationToken cancellationToken)
    {
        var coupon = await dbContext.Coupons
        .FirstOrDefaultAsync(c => c.Id.Value == command.CouponId, cancellationToken);

        if (coupon == null)
            throw new NotFoundException($"Coupon with ID {command.CouponId} not found.");

        coupon.UpdateDetails(
            command.Name,
            command.Description,
            Discountable.Of(command.Discountable.FlatAmount, command.Discountable.Percentage),
            command.StartDate,
            command.EndDate,
            command.MaxUses,
            command.MaxUsesPerCustomer,
            command.MinimumOrderValue,
            command.IsFirstTimeOrderOnly
        );

        return new UpdateCouponResult(true);
    }
}
