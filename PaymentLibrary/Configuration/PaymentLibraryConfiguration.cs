namespace PaymentLibrary.Configuration;

public class PaymentLibraryConfiguration
{
    public bool EnableNotifications { get; set; } = true;
    public int MaxRetryAttempts { get; set; } = 3;
    public string DefaultCurrency { get; set; } = "USD";
}