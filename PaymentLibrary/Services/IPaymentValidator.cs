using PaymentLibrary.Models;

namespace PaymentLibrary.Services;

internal interface IPaymentValidator
{
    ValidationResult Validate(PaymentRequest request);
}

internal class ValidationResult
{
    public bool IsValid { get; set; }
    public string ErrorMessage { get; set; }
}