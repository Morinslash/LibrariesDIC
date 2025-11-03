using PaymentLibrary.External;
using PaymentLibrary.Models;
using PaymentLibrary.Repositories;

namespace PaymentLibrary.Services;

public interface IPaymentProcessor
{
    /// <summary>
    /// Main service exposed to consumers
    /// </summary>
    Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request);

    Task<int> GetTotalProcessedPaymentsAsync();
}

internal class PaymentProcessor(
    IPaymentGateway paymentGateway,
    INotificationService notificationService,
    IPaymentValidator validator,
    IReceiptGenerator receiptGenerator,
    IPaymentRepository repository)
    : IPaymentProcessor
{
    private readonly IPaymentGateway _paymentGateway =
        paymentGateway ?? throw new ArgumentNullException(nameof(paymentGateway));

    private readonly INotificationService _notificationService =
        notificationService ?? throw new ArgumentNullException(nameof(notificationService));

    private readonly IPaymentValidator _validator = validator ?? throw new ArgumentNullException(nameof(validator));

    private readonly IReceiptGenerator _receiptGenerator =
        receiptGenerator ?? throw new ArgumentNullException(nameof(receiptGenerator));

    private readonly IPaymentRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public async Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request)
    {
        // Step 1: Validate using internal validator
        var validation = _validator.Validate(request);
        if (!validation.IsValid)
        {
            return new PaymentResult
            {
                Success = false,
                ErrorMessage = validation.ErrorMessage
            };
        }

        var paymentId = Guid.NewGuid().ToString("N");

        try
        {
            // Step 2: Process payment via external gateway
            var gatewayResponse = await _paymentGateway.ProcessPaymentAsync(
                request.Amount,
                request.Currency,
                request.PaymentToken);

            if (!gatewayResponse.Success)
            {
                await _repository.SavePaymentAsync(paymentId, request, gatewayResponse.TransactionId, false);

                await _notificationService.SendNotificationAsync(
                    request.UserEmail,
                    "Payment Failed",
                    $"Your payment of {request.Amount} {request.Currency} failed: {gatewayResponse.ErrorMessage}");

                return new PaymentResult
                {
                    Success = false,
                    PaymentId = paymentId,
                    ErrorMessage = gatewayResponse.ErrorMessage
                };
            }

            // Step 3: Generate receipt using internal service
            var receipt = _receiptGenerator.Generate(request, paymentId, gatewayResponse.TransactionId);

            // Step 4: Save to internal repository
            await _repository.SavePaymentAsync(paymentId, request, gatewayResponse.TransactionId, true);

            // Step 5: Send notification via external service
            await _notificationService.SendNotificationAsync(
                request.UserEmail,
                "Payment Successful",
                $"Your payment of {request.Amount} {request.Currency} was successful.\n\n{receipt.FormattedReceipt}");

            return new PaymentResult
            {
                Success = true,
                PaymentId = paymentId,
                TransactionId = gatewayResponse.TransactionId,
                Receipt = receipt
            };
        }
        catch (Exception ex)
        {
            await _repository.SavePaymentAsync(paymentId, request, null, false);

            return new PaymentResult
            {
                Success = false,
                PaymentId = paymentId,
                ErrorMessage = $"Payment processing failed: {ex.Message}"
            };
        }
    }

    public Task<int> GetTotalProcessedPaymentsAsync()
    {
        return _repository.GetPaymentCountAsync();
    }
}