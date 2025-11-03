using PaymentLibrary.External;

namespace ConsumerApp.ExternalServices;

public class EmailNotificationService : INotificationService
{
    public async Task SendNotificationAsync(string recipient, string subject, string message)
    {
        Console.WriteLine($"\n[Email Service] Sending email to: {recipient}");
        Console.WriteLine($"[Email Service] Subject: {subject}");
        Console.WriteLine($"[Email Service] Message:\n{message}\n");

        // Simulate sending email
        await Task.Delay(200);

        Console.WriteLine("[Email Service] Email sent successfully!\n");
    }
}