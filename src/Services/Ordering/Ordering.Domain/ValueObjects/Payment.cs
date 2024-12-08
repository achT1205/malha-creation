﻿namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string CardHolderName { get; } = default!;
    public string CardNumber { get; } = default!;
    public string Expiration { get; } = default!;
    public string CVV { get; } = default!;
    public int PaymentMethod { get; } = default!;

    protected Payment()
    {
    }

    private Payment(string cardHolderName, string cardNumber, string expiration, string cvv, int paymentMethod)
    {
        CardHolderName = cardHolderName;
        CardNumber = cardNumber;
        Expiration = expiration;
        CVV = cvv;
        PaymentMethod = paymentMethod;
    }

    public static Payment Of(string cardHolderName, string cardNumber, string expiration, string cvv, int paymentMethod)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cardHolderName);
        ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(cvv);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3);

        return new Payment(cardHolderName, cardNumber, expiration, cvv, paymentMethod);
    }
}