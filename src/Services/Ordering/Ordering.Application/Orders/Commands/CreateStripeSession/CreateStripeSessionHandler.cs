
using Stripe.Checkout;
using Stripe;
using MassTransit;
using Ordering.Application.Orders.Queries.GetOrdersById;


namespace Ordering.Application.Orders.Commands.CreateStripeSession;

public record CreateStripeSessionCommand : ICommand<CreateStripeSessionResult>
{

    public string? StripeSessionUrl { get; set; }
    public string? StripeSessionId { get; set; }
    public string ApprovedUrl { get; set; } = default!;
    public string CancelUrl { get; set; } = default!;
    public OrderBasket Basket { get; set; } = default!;
};

public record CreateStripeSessionResult();

public class CreateStripeSessionCommandHandler : ICommandHandler<CreateStripeSessionCommand, CreateStripeSessionResult>
{
    public async Task<CreateStripeSessionResult> Handle(CreateStripeSessionCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var options = new SessionCreateOptions
            {
                SuccessUrl = command.ApprovedUrl,
                CancelUrl = command.CancelUrl,
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",

            };

            //var DiscountsObj = new List<SessionDiscountOptions>()
            //    {
            //        new SessionDiscountOptions
            //        {
            //            Coupon=stripeRequestDto.OrderHeader.CouponCode
            //        }
            //    };

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

            //if (stripeRequestDto.OrderHeader.Discount > 0)
            //{
            //    options.Discounts = DiscountsObj;
            //}
            var service = new SessionService();
            Session session = service.Create(options);
            //command.StripeSessionUrl = session.Url;
            //var cmd = GetOrdersByIdQuery()
            //var order  = 
            //OrderHeader orderHeader = _db.OrderHeaders.First(u => u.OrderHeaderId == stripeRequestDto.OrderHeader.OrderHeaderId);
            //orderHeader.StripeSessionId = session.Id;
            //_db.SaveChanges();
            //_response.Result = stripeRequestDto;
        }
        catch (Exception ex) { 
        
        }

        return new CreateStripeSessionResult();
    }
}
