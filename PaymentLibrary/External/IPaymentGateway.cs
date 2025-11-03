namespace PaymentLibrary.External;

public interface IPaymentGateway
{
    /// <summary>
    /// External dependency: Consumer must provide implementation for their payment gateway
    /// </summary>
    Task<PaymentGatewayResponse> ProcessPaymentAsync(decimal amount, string currency, string token);
}

public class PaymentGatewayResponse
{
    public bool Success { get; set; }
    public string TransactionId { get; set; }
    public string ErrorMessage { get; set; }
}