using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Geta.Integration.Omnium.Sdk;

/// <summary>
/// No one is going read this anyway
/// </summary>
public static class IServiceCollectionExtensions
{
    /// <summary>
    /// Configures Omnium Integration required services
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="configureHttpMessageHandler">Callback if you need to configure HttpClient (like specifying proxy settings etc.)</param>
    /// <returns>Service collection for chaining</returns>
    public static IServiceCollection AddOmniumIntegration(
        this IServiceCollection services,
        Func<HttpMessageHandler>? configureHttpMessageHandler = null)
    {
        services.AddMemoryCache();

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

        var httpBuilder = services
            .AddHttpClient<IClient, Client>((serviceProvider, httpClient) =>
            {
                var configuration = serviceProvider.GetRequiredService<IOptions<OmniumConfiguration>>();
                httpClient.BaseAddress = new Uri(configuration.Value.BaseAddress);
            })
            .AddPolicyHandler(ExceptionPolicies.GetRetryOnTooManyRequestsPolicy())
            .AddHttpMessageHandler<TokenHandler>();

        if (configureHttpMessageHandler != null)
        {
            httpBuilder.ConfigurePrimaryHttpMessageHandler(configureHttpMessageHandler);
        }

        return services;
    }
}
