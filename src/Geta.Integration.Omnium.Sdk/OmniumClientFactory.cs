using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Geta.Integration.Omnium.Sdk;

/// <inheritdoc />
public class OmniumClientFactory : IOmniumClientFactory
{
    private readonly IServiceProvider _sp;

    internal const string ScopedHttpClientName = "ScopedOmniumHttpClient";

    /// <summary>
    /// Creates new instance of <see cref="OmniumClientFactory"/>.
    /// </summary>
    /// <param name="sp">Service provider.</param>
    public OmniumClientFactory(IServiceProvider sp)
    {
        _sp = sp;
    }

    /// <inheritdoc />
    public IClient? CreateClient(string tenantId, IOptions<OmniumConfiguration> config)
    {
        var ctx = _sp.GetRequiredService<TenantContext>();
        ctx.Set(tenantId, config);

        var factory = _sp.GetRequiredService<IHttpClientFactory>();
        var client = factory.CreateClient(ScopedHttpClientName);

        client.BaseAddress = new Uri(config.Value.BaseAddress);

        return ActivatorUtilities.CreateInstance(_sp, typeof(Client), client) as IClient;
    }
}
