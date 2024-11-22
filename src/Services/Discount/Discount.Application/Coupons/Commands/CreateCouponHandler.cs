namespace Discount.Application.Coupons.Commands;

public record CreateCouponCommand : ICommand<CreateCouponResult>
{
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

    public List<Guid> CustomerIds { get; init; } = new();
    public List<Guid> ProductIds { get; init; } = new();
}
public record CreateCouponResult(Guid CouponId, string CouponCode, string Name);
public class CreateCouponCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateCouponCommand, CreateCouponResult>
{
    public async Task<CreateCouponResult> Handle(CreateCouponCommand command, CancellationToken cancellationToken)
    {
        var coupon = Coupon.Create(
               CouponId.Of(Guid.NewGuid()),
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
               command.IsActive);

        foreach (var customerId in command.CustomerIds)
        {
            coupon.AddSpecificCustomer(CustomerId.Of(customerId));
        }

        foreach (var productId in command.ProductIds)
        {
            coupon.AddApplicableProduct(ProductId.Of(productId));
        }

        await dbContext.Coupons.AddAsync(coupon);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateCouponResult(coupon.Id.Value, coupon.Code.Value, coupon.Name); 
    }
}
