﻿namespace Ordering.API.Models;

public static class StripeEventTypes
{
    public const string PaymentIntentSucceeded = "payment_intent.succeeded";
    public const string PaymentIntentFailed = "payment_intent.payment_failed";
}
