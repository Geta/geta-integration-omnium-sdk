using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Geta.Integration.Omnium.Sdk;

/// <inheritdoc />
public class TokenHandler : BaseTokenHandler
{
    /// <inheritdoc />
    public TokenHandler(IAuthService authService, IOptions<OmniumConfiguration> configuration, IMemoryCache cache) : base(authService, cache)
    {
        ClientId = configuration.Value.ClientId;
        ClientSecret = configuration.Value.ClientSecret;
    }
}
