using PaymentLibrary.Models;

namespace PaymentLibrary.Services;

internal class PaymentValidator : IPaymentValidator
{
    public ValidationResult Validate(PaymentRequest request)
    {
        if (request == null)
            return new ValidationResult { IsValid = false, ErrorMessage = "Payment request is null" };

        if (request.Amount <= 0)
            return new ValidationResult { IsValid = false, ErrorMessage = "Amount must be greater than zero" };

        if (string.IsNullOrWhiteSpace(request.Currency))
            return new ValidationResult { IsValid = false, ErrorMessage = "Currency is required" };

        if (string.IsNullOrWhiteSpace(request.PaymentToken))
            return new ValidationResult { IsValid = false, ErrorMessage = "Payment token is required" };

        if (string.IsNullOrWhiteSpace(request.UserEmail))
            return new ValidationResult { IsValid = false, ErrorMessage = "User email is required" };

        return new ValidationResult { IsValid = true };
    }
}