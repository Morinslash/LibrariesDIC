using System.Text;
using PaymentLibrary.Models;

namespace PaymentLibrary.Services;

internal class ReceiptGenerator : IReceiptGenerator
{
    public Receipt Generate(PaymentRequest request, string paymentId, string transactionId)
    {
        var receipt = new Receipt
        {
            ReceiptId = Guid.NewGuid().ToString("N"),
            PaymentId = paymentId,
            Amount = request.Amount,
            Currency = request.Currency,
            Timestamp = DateTime.UtcNow,
            Description = request.Description
        };

        var sb = new StringBuilder();
        sb.AppendLine("========== RECEIPT ==========");
        sb.AppendLine($"Receipt ID: {receipt.ReceiptId}");
        sb.AppendLine($"Payment ID: {paymentId}");
        sb.AppendLine($"Transaction ID: {transactionId}");
        sb.AppendLine($"Amount: {request.Amount:N2} {request.Currency}");
        sb.AppendLine($"Description: {request.Description}");
        sb.AppendLine($"Date: {receipt.Timestamp:yyyy-MM-dd HH:mm:ss} UTC");
        sb.AppendLine("=============================");

        receipt.FormattedReceipt = sb.ToString();

        return receipt;
    }
}