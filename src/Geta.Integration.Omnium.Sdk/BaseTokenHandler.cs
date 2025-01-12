using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.Caching.Memory;

namespace Geta.Integration.Omnium.Sdk;

public abstract class BaseTokenHandler : DelegatingHandler
{
    private readonly IAuthService _authService;
    private readonly IMemoryCache _cache;
    protected string CacheKey = "__Omnium_SDK_AccessToken";
    protected string ClientId;
    protected string ClientSecret;

    public BaseTokenHandler(IAuthService authService, IMemoryCache cache)
    {
        _authService = authService;
        _cache = cache;
    }

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

    private async Task<AccessToken> GetTokenAsync()
    {
        if (_cache.TryGetValue(CacheKey, out AccessToken result))
        {
            return result;
        }

        return await RefreshTokenAsync();
    }

    private async Task<AccessToken> RefreshTokenAsync()
    {
        var token = await _authService.GetTokenAsync(ClientId, ClientSecret);
        var accessToken = new AccessToken("Bearer", token);

        _cache.Set(CacheKey, accessToken);

        return accessToken;
    }
}
