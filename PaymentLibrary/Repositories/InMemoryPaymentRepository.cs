using System.Collections.Concurrent;
using PaymentLibrary.Models;

namespace PaymentLibrary.Repositories;

internal class InMemoryPaymentRepository : IPaymentRepository
{
    private readonly ConcurrentDictionary<string, PaymentRecord> _payments = new();

    public Task SavePaymentAsync(string paymentId, PaymentRequest request, string transactionId, bool success)
    {
        var record = new PaymentRecord
        {
            PaymentId = paymentId,
            UserId = request.UserId,
            Amount = request.Amount,
            Currency = request.Currency,
            TransactionId = transactionId,
            Success = success
        };

        _payments[paymentId] = record;
        return Task.CompletedTask;
    }

    public Task<int> GetPaymentCountAsync()
    {
        return Task.FromResult(_payments.Count);
    }

    private class PaymentRecord
    {
        public string PaymentId { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string TransactionId { get; set; }
        public bool Success { get; set; }
    }
}