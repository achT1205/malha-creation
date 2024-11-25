using Stripe;
using Stripe.Checkout;

namespace Ordering.Application.Orders.Commands.ValidateStripeSession
{
    public record ValidateStripeSessionCommand(Guid OrderId) : ICommand<ValidateStripeSessionResult>;
    public record ValidateStripeSessionResult(bool IsSuccess);

    public class ValidateStripeSessionCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<ValidateStripeSessionCommand, ValidateStripeSessionResult>
    {
        public async Task<ValidateStripeSessionResult> Handle(ValidateStripeSessionCommand command, CancellationToken cancellationToken)
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
                    return new ValidateStripeSessionResult(true);
                }
                else
                    return new ValidateStripeSessionResult(false);

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }

}
