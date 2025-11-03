using PaymentLibrary.Models;

namespace PaymentLibrary.Repositories;

internal interface IPaymentRepository
{
    Task SavePaymentAsync(string paymentId, PaymentRequest request, string transactionId, bool success);
    Task<int> GetPaymentCountAsync();
}