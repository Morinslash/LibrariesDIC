using PaymentLibrary.Models;

namespace PaymentLibrary.Services;

internal interface IReceiptGenerator
{
    Receipt Generate(PaymentRequest request, string paymentId, string transactionId);
}