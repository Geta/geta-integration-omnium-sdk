using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Geta.Integration.Omnium.Sdk;

/// <summary>
/// A token handler that uses tenant context to set the appropriate tenant configuration.
/// </summary>
public class ScopedTokenHandler : BaseTokenHandler
{
    private readonly TenantContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="ScopedTokenHandler"/> class.
    /// </summary>
    /// <param name="authService">The authentication service.</param>
    /// <param name="cache">The memory cache.</param>
    /// <param name="context">The tenant context.</param>
    public ScopedTokenHandler(
        IAuthService authService,
        IMemoryCache cache,
        TenantContext context) : base(authService, cache)
    {
        _context = context;
    }

    /// <summary>
    /// Sends an HTTP request with the appropriate tenant configuration.
    /// </summary>
    /// <param name="request">The HTTP request message.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>The HTTP response message.</returns>
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        SetTenant(_context.TenantId!, _context.TenantConfiguration!);

        return base.SendAsync(request, cancellationToken);
    }

    internal void SetTenant(string tenantId, IOptions<OmniumConfiguration> tenantConfiguration)
    {
        ArgumentNullException.ThrowIfNull(tenantId);
        ArgumentNullException.ThrowIfNull(tenantConfiguration);

        CacheKey += $"__{tenantId}";
        ClientId = tenantConfiguration.Value.ClientId;
        ClientSecret = tenantConfiguration.Value.ClientSecret;
    }
}
