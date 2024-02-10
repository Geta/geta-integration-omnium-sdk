using System.Net.Http.Headers;
using Microsoft.Extensions.Options;

namespace Geta.Integration.Omnium.Sdk;

public class TokenHandler : DelegatingHandler
{
    private readonly AuthService _authService;
    private readonly IOptions<OmniumConfiguration> _configuration;

    public TokenHandler(AuthService authService, IOptions<OmniumConfiguration> configuration)
    {
        _authService = authService;
        _configuration = configuration;
    }

    /// <summary>
    /// When SendAsync is executed this method will be called.
    /// Get the token and sets AuthenticationHeaderValue in the request header
    /// </summary>
    /// <param name="request">The request being intercepted</param>
    /// <param name="cancellationToken">Default CancellationToken</param>
    /// <returns></returns>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var result = await _authService.GetTokenAsync(_configuration.Value.ClientId, _configuration.Value.ClientSecret);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result);

        return await base.SendAsync(request, cancellationToken);
    }
}
