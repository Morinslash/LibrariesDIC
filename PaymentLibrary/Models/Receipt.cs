namespace PaymentLibrary.Models;

public class Receipt
{
    public string ReceiptId { get; set; }
    public string PaymentId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public DateTime Timestamp { get; set; }
    public string Description { get; set; }
    public string FormattedReceipt { get; set; }
}