using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Geta.Integration.Omnium.Sdk;

/// <inheritdoc />
public class TokenHandler : DelegatingHandler
{
    private readonly AuthService _authService;
    private readonly IMemoryCache _cache;
    private string _cacheKey = "__Omnium_SDK_AccessToken";
    private string _clientId;
    private string _clientSecret;

    /// <inheritdoc />
    public TokenHandler(AuthService authService, IOptions<OmniumConfiguration> configuration, IMemoryCache cache)
    {
        _authService = authService;
        _cache = cache;
        _clientId = configuration.Value.ClientId;
        _clientSecret = configuration.Value.ClientSecret;
    }

    /// <inheritdoc />
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await GetTokenAsync();
        request.Headers.Authorization = new AuthenticationHeaderValue(token.Scheme, token.Token);
        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode is not (HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden))
        {
            return response;
        }

        token = await RefreshTokenAsync();
        request.Headers.Authorization = new AuthenticationHeaderValue(token.Scheme, token.Token);
        response = await base.SendAsync(request, cancellationToken);

        return response;
    }

    internal void SetTenant(string tenantId, IOptions<OmniumConfiguration> tenantConfiguration)
    {
        ArgumentNullException.ThrowIfNull(tenantId);
        ArgumentNullException.ThrowIfNull(tenantConfiguration);

        _cacheKey += $"__{tenantId}";
        _clientId = tenantConfiguration.Value.ClientId;
        _clientSecret = tenantConfiguration.Value.ClientSecret;
    }

    private async Task<AccessToken> RefreshTokenAsync()
    {
        var token = await _authService.GetTokenAsync(_clientId, _clientSecret);
        var accessToken = new AccessToken("Bearer", token);

        _cache.Set(_cacheKey, accessToken);

        return accessToken;
    }

    private async Task<AccessToken> GetTokenAsync()
    {
        if (_cache.TryGetValue(_cacheKey, out AccessToken result))
        {
            return result;
        }

        return await RefreshTokenAsync();
    }
}
