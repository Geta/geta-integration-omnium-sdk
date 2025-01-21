using Microsoft.Extensions.Options;

namespace Geta.Integration.Omnium.Sdk;

/// <summary>
/// Provides context for the current tenant, including tenant ID and configuration.
/// </summary>
public class TenantContext
{
    private static readonly AsyncLocal<TenantContextHolder> TenantContextCurrent = new();

    /// <summary>
    /// Gets the current tenant identifier.
    /// </summary>
    public string? TenantId
    {
        get
        {
            return TenantContextCurrent.Value?._tenantName;
        }
    }

    /// <summary>
    /// Gets the configuration options for the current tenant.
    /// </summary>
    public IOptions<OmniumConfiguration>? TenantConfiguration
    {
        get
        {
            return TenantContextCurrent.Value?._tenantConfiguration;
        }
    }

    /// <summary>
    /// Sets the tenant context with the specified tenant ID and configuration.
    /// </summary>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <param name="config">The configuration options for the tenant.</param>
    public void Set(string? tenantId, IOptions<OmniumConfiguration>? config)
    {
        var holder = TenantContextCurrent.Value;
        if (holder != null)
        {
            // Clear current HttpContext trapped in the AsyncLocals, as it's done.
            holder._tenantName = null;
        }

        if (tenantId != null && config != null)
        {
            // Use an object indirection to hold the HttpContext in the AsyncLocal,
            // so it can be cleared in all ExecutionContexts when it's cleared.
            TenantContextCurrent.Value = new TenantContextHolder
            {
                _tenantName = tenantId,
                _tenantConfiguration = config
            };
        }
    }

    private sealed class TenantContextHolder
    {
        public string? _tenantName;
        public IOptions<OmniumConfiguration>? _tenantConfiguration;
    }
}
