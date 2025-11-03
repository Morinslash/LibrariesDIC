namespace PaymentLibrary.Models;

public class PaymentRequest
{
    public string UserId { get; set; }
    public string UserEmail { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string PaymentToken { get; set; }
    public string Description { get; set; }
}