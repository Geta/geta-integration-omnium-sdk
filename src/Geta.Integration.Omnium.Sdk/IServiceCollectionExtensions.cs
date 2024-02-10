using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Geta.Integration.Omnium.Sdk;

public static class IServiceCollectionExtensions
{
    /// <summary>
    /// Configures Omnium Integration required services
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <returns>Service collection for chaining</returns>
    public static IServiceCollection AddOmniumIntegration(this IServiceCollection services)
    {
        services.AddScoped<AuthService>();
        services.AddTransient<TokenHandler>();

        services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<OmniumConfiguration>, OmniumConfigurationConfigurer>());
        services.TryAddEnumerable(ServiceDescriptor.Singleton<IValidateOptions<OmniumConfiguration>, OmniumConfigurationValidator>());

        services.AddHttpClient(
            "Omnium_LoginClient",
            (serviceProvider, httpClient) =>
            {
                var configuration = serviceProvider.GetRequiredService<IOptions<OmniumConfiguration>>();
                httpClient.BaseAddress = new Uri(configuration.Value.BaseAddress);
            });
        
        services
            .AddHttpClient<IClient, Client>((serviceProvider, httpClient) =>
            {
                var configuration = serviceProvider.GetRequiredService<IOptions<OmniumConfiguration>>();
                httpClient.BaseAddress = new Uri(configuration.Value.BaseAddress);
            })
            .AddHttpMessageHandler<TokenHandler>();

        return services;
    }
}
