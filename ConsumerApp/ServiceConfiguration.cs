using ConsumerApp.ExternalServices;
using Microsoft.Extensions.DependencyInjection;
using PaymentLibrary.Configuration;

namespace ConsumerApp;

public static class ServiceConfiguration
{
    public static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // COMPILE-TIME SAFE: Must specify both types
        services.AddPaymentLibrary<StripePaymentGateway, EmailNotificationService>(config =>
        {
            config.EnableNotifications = true;
            config.MaxRetryAttempts = 3;
            config.DefaultCurrency = "EUR";
        });

        return services.BuildServiceProvider();
    }
}
