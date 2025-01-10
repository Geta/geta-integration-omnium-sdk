using Microsoft.Extensions.Options;

namespace Geta.Integration.Omnium.Sdk;

/// <summary>
/// Used to multi-tenant client creation.
/// </summary>
public interface IOmniumClientFactory
{
    /// <summary>
    /// Creates a client for a specific tenant.
    /// </summary>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <param name="config">The configuration options for Omnium.</param>
    /// <returns>An instance of <see cref="IClient"/>.</returns>
    IClient? CreateClient(string tenantId, IOptions<OmniumConfiguration> config);
}
