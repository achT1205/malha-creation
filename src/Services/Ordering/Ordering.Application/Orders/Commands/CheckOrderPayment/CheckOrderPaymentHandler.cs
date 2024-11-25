using Stripe;
using Stripe.Checkout;

namespace Ordering.Application.Orders.Commands.CheckOrderPayment
{
    public record CheckOrderPaymentCommand(Guid OrderId) : ICommand<CheckOrderPaymentResult>;
    public record CheckOrderPaymentResult(bool IsSuccess);

    public class CheckOrderPaymentCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<CheckOrderPaymentCommand, CheckOrderPaymentResult>
    {
        public async Task<CheckOrderPaymentResult> Handle(CheckOrderPaymentCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var order = await dbContext.Orders
                    .SingleOrDefaultAsync(t => t.Id == OrderId.Of(command.OrderId), cancellationToken);
                if (order is null)
                {
                    throw new OrderNotFoundException(command.OrderId);
                }

                var service = new SessionService();
                Session session = service.Get(order.StripeSessionId);

                var paymentIntentService = new PaymentIntentService();
                PaymentIntent paymentIntent = paymentIntentService.Get(session.PaymentIntentId);
                if (paymentIntent.Status == "succeeded")
                {
                    //then payment was successful
                    order.SetPaymentIntentId(paymentIntent.Id);
                    await dbContext.SaveChangesAsync(cancellationToken);
                    return new CheckOrderPaymentResult(true);
                }
                else
                    return new CheckOrderPaymentResult(false);

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }

}
