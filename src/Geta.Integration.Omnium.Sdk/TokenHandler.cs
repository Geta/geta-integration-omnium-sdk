using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Geta.Integration.Omnium.Sdk;

/// <inheritdoc />
public class TokenHandler : DelegatingHandler
{
    private const string OmniumAccessTokenCacheKey = "__Omnium_SDK_AccessToken";
    private readonly AuthService _authService;
    private readonly IOptions<OmniumConfiguration> _configuration;
    private readonly IMemoryCache _cache;

    /// <inheritdoc />
    public TokenHandler(AuthService authService, IOptions<OmniumConfiguration> configuration, IMemoryCache cache)
    {
        _authService = authService;
        _configuration = configuration;
        _cache = cache;
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

    private async Task<AccessToken> RefreshTokenAsync()
    {
        var token = await _authService.GetTokenAsync(_configuration.Value.ClientId, _configuration.Value.ClientSecret);
        var accessToken = new AccessToken("Bearer", token);

        _cache.Set(OmniumAccessTokenCacheKey, accessToken);

        return accessToken;
    }

    private async Task<AccessToken> GetTokenAsync()
    {
        if (_cache.TryGetValue(OmniumAccessTokenCacheKey, out AccessToken result))
        {
            return result;
        }
        
        return await RefreshTokenAsync();
    }
}
