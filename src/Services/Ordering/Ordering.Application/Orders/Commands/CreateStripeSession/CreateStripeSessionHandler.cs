
using Stripe.Checkout;
using Ordering.Application.Orders.Queries.GetOrdersById;


namespace Ordering.Application.Orders.Commands.CreateStripeSession;

public record CreateStripeSessionCommand : ICommand<CreateStripeSessionResult>
{
    public string ApprovedUrl { get; set; } = default!;
    public string CancelUrl { get; set; } = default!;
    public Guid OrderId { get; set; }
    public OrderBasket Basket { get; set; } = default!;
};

public record CreateStripeSessionResult(bool IsSuccess);

public class CreateStripeSessionCommandHandler(ISender sender, IApplicationDbContext dbContext) : ICommandHandler<CreateStripeSessionCommand, CreateStripeSessionResult>
{
    public async Task<CreateStripeSessionResult> Handle(CreateStripeSessionCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var order = await dbContext.Orders
                .SingleOrDefaultAsync(t => t.Id == OrderId.Of(command.OrderId), cancellationToken);
            if (order is null)
            {
                throw new OrderNotFoundException(command.OrderId);
            }

            var sessionLineItems = command.Basket.Items.Select(item => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.ProductName,
                        Images = [item.CoverImage],
                        Description = item.Coupon.Description 
                    },
                    UnitAmountDecimal = (long)(item.Price * 100), // Stripe expects amounts in cents
                },
                Quantity = item.Quantity,
            }).ToList();

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = sessionLineItems,
                Mode = "payment",
                SuccessUrl = "https://yourdomain.com/success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "https://yourdomain.com/cancel",
                Metadata = new Dictionary<string, string>
                {
                    { "OrderId", command.OrderId.ToString() },
                    { "CustomerId", command.Basket.UserId.ToString() }
                }
            };

            if (!string.IsNullOrWhiteSpace(command.Basket.Coupon.CouponCode))
            {
                options.Discounts = new List<SessionDiscountOptions>()
                {
                    new SessionDiscountOptions
                    {
                        Coupon = command.Basket.Coupon.CouponCode
                    }
                };
            }
            var service = new SessionService();
            Session session = service.Create(options);
            var cmd = new GetOrdersByIdQuery(command.OrderId);
            var result = await sender.Send(cmd);
            order.SetStripeSessionId(session.Id);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {

        }

        return new CreateStripeSessionResult(true);
    }
}
