
using Microsoft.Extensions.DependencyInjection;
using PaymentLibrary.Models;
using PaymentLibrary.Services;

namespace ConsumerApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Payment Processing Library Demo ===\n");

            // Configure all dependencies
            var serviceProvider = ServiceConfiguration.ConfigureServices();

            // Get the payment processor and use it
            var paymentProcessor = serviceProvider.GetRequiredService<IPaymentProcessor>();

            await RunPaymentDemo(paymentProcessor);
        }

        private static async Task RunPaymentDemo(IPaymentProcessor paymentProcessor)
        {
            Console.WriteLine("Processing payments...\n");
            Console.WriteLine("=".PadRight(50, '='));

            // Process first payment
            var request1 = new PaymentRequest
            {
                UserId = "user_123",
                UserEmail = "john.doe@example.com",
                Amount = 99.99m,
                Currency = "USD",
                PaymentToken = "tok_visa_4242",
                Description = "Premium Subscription - Monthly"
            };

            Console.WriteLine("\nProcessing Payment #1...");
            var result1 = await paymentProcessor.ProcessPaymentAsync(request1);
            DisplayResult(result1);

            // Process second payment
            var request2 = new PaymentRequest
            {
                UserId = "user_456",
                UserEmail = "jane.smith@example.com",
                Amount = 49.99m,
                Currency = "USD",
                PaymentToken = "tok_visa_5555",
                Description = "Basic Subscription - Monthly"
            };

            Console.WriteLine("\nProcessing Payment #2...");
            var result2 = await paymentProcessor.ProcessPaymentAsync(request2);
            DisplayResult(result2);

            // Show statistics
            var totalPayments = await paymentProcessor.GetTotalProcessedPaymentsAsync();
            Console.WriteLine($"\n{new string('=', 50)}");
            Console.WriteLine($"Total payments processed: {totalPayments}");
            Console.WriteLine($"{new string('=', 50)}");
        }

        static void DisplayResult(PaymentResult result)
        {
            Console.WriteLine($"\n--- Payment Result ---");
            Console.WriteLine($"Success: {result.Success}");
            Console.WriteLine($"Payment ID: {result.PaymentId}");

            if (result.Success)
            {
                Console.WriteLine($"Transaction ID: {result.TransactionId}");
                Console.WriteLine($"\n{result.Receipt.FormattedReceipt}");
            }
            else
            {
                Console.WriteLine($"Error: {result.ErrorMessage}");
            }
        }
    }
}