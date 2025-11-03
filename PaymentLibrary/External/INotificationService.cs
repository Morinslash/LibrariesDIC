namespace PaymentLibrary.External;

public interface INotificationService
{
    /// <summary>
    /// External dependency: Consumer must provide implementation for notifications
    /// </summary>
    Task SendNotificationAsync(string recipient, string subject, string message);
}