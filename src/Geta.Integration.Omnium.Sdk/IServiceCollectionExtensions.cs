using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Geta.Integration.Omnium.Sdk;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddOmniumIntegration(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<OmniumConfiguration>? action = null)
    {
        // TODO: bind against config file
        var settings = new OmniumConfiguration();
        action?.Invoke(settings);
        services.AddSingleton(_ => Options.Create(settings));

        // TODO: validate configuration
        //services.AddOptions<OmniumConfiguration>()
        //    .Bind(configuration.GetSection(nameof(OmniumConfiguration)))
        //    .ValidateDataAnnotations()
        //    .ValidateOnStart();

        // TODO: configure Omnium clients
        services.AddScoped<AuthService>();
        services.AddTransient<TokenHandler>();

        services.AddHttpClient("Omnium_LoginClient", options => { options.BaseAddress = new Uri(settings.BaseAddress); });
        services
            .AddHttpClient<IClient, Client>(options => { options.BaseAddress = new Uri(settings.BaseAddress); })
            .AddHttpMessageHandler<TokenHandler>();

        return services;
    }
}
