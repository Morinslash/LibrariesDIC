using PaymentLibrary.External;

namespace ConsumerApp.ExternalServices;

public class StripePaymentGateway : IPaymentGateway
{
    public async Task<PaymentGatewayResponse> ProcessPaymentAsync(decimal amount, string currency, string token)
    {
        Console.WriteLine($"[Stripe] Processing payment: {amount} {currency}");
        Console.WriteLine($"[Stripe] Using token: {token}");

        // Simulate API call to Stripe
        await Task.Delay(500);

        // Simulate 90% success rate
        var random = new Random();
        var success = random.Next(100) < 90;

        if (success)
        {
            var transactionId = $"stripe_txn_{Guid.NewGuid().ToString("N")[..12]}";
            Console.WriteLine($"[Stripe] Success! Transaction ID: {transactionId}");

            return new PaymentGatewayResponse
            {
                Success = true,
                TransactionId = transactionId
            };
        }
        else
        {
            Console.WriteLine("[Stripe] Payment declined by bank");
            return new PaymentGatewayResponse
            {
                Success = false,
                ErrorMessage = "Payment declined by bank"
            };
        }
    }
}