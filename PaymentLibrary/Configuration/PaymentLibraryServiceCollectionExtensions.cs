using Microsoft.Extensions.DependencyInjection;
using PaymentLibrary.External;
using PaymentLibrary.Repositories;
using PaymentLibrary.Services;

namespace PaymentLibrary.Configuration;

public static class PaymentLibraryServiceCollectionExtensions
{
    /// <summary>
    /// Adds the PaymentLibrary services to the DI container with required external dependencies.
    /// </summary>
    public static IServiceCollection AddPaymentLibrary<TPaymentGateway, TNotificationService>(
        this IServiceCollection services,
        Action<PaymentLibraryConfiguration>? configureOptions = null)
        where TPaymentGateway : class, IPaymentGateway
        where TNotificationService : class, INotificationService
    {
        ArgumentNullException.ThrowIfNull(services);

        // Register external dependencies
        services.AddSingleton<IPaymentGateway, TPaymentGateway>();
        services.AddSingleton<INotificationService, TNotificationService>();

        // Register configuration
        var config = new PaymentLibraryConfiguration();
        configureOptions?.Invoke(config);
        services.AddSingleton(config);

        // Register internal services
        services.AddSingleton<IPaymentRepository, InMemoryPaymentRepository>();
        services.AddTransient<IPaymentValidator, PaymentValidator>();
        services.AddTransient<IReceiptGenerator, ReceiptGenerator>();
        
        // Register the main processor
        services.AddTransient<IPaymentProcessor, PaymentProcessor>();

        return services;
    }
}
