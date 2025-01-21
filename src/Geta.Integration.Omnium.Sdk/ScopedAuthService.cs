namespace Geta.Integration.Omnium.Sdk;

public class ScopedAuthService : IAuthService
{
    private readonly HttpClient _loginClient;

    /// <summary>
    ///     HttpClient for getting Bearer token
    /// </summary>
    /// <param name="clientFactory">Injected IHttpClientFactory</param>
    public ScopedAuthService(IHttpClientFactory clientFactory, TenantContext context)
    {
        _loginClient = clientFactory.CreateClient("ScopedOmnium_LoginClient");
        _loginClient.BaseAddress = new Uri(context.TenantConfiguration!.Value.BaseAddress);
    }

    /// <summary>
    ///     Sends a request to /api/Token with ClientId and Secret
    ///     to get the Baerer token string
    /// </summary>
    /// <returns>Token string used for authentication</returns>
    public async Task<string> GetTokenAsync(string clientId, string clientSecret)
    {
        var result = await _loginClient.PostAsync($"/api/Token?clientId={clientId}&clientSecret={clientSecret}&returnAsJson=false", null);
        var content = await result.Content.ReadAsStringAsync();

        return content;
    }
}
