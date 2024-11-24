
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

            var options = new SessionCreateOptions
            {
                SuccessUrl = command.ApprovedUrl,
                CancelUrl = command.CancelUrl,
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",

            };

            var DiscountsObj = new List<SessionDiscountOptions>();
            //foreach (var item in command.Basket.Items)
            //{
            //    if (item.Coupon != null && !string.IsNullOrWhiteSpace(item.Coupon.CouponCode))
            //    {
            //        DiscountsObj.Add(new SessionDiscountOptions
            //        {
            //            Coupon = item.Coupon.CouponCode
            //        });
            //    }

            //}

            foreach (var item in command.Basket.Items)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * 100), // $20.99 -> 2099
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.ProductName,
                            Images = [item.CoverImage],
                        }
                    },
                    Quantity = item.Quantity
                };

                options.LineItems.Add(sessionLineItem);
            }

            if (DiscountsObj.Any())
            {
                options.Discounts = DiscountsObj;
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
