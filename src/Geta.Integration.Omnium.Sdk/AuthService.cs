namespace Geta.Integration.Omnium.Sdk;

public class AuthService
{
    private readonly HttpClient _loginClient;

    /// <summary>
    ///     HttpClient for getting Bearer token
    /// </summary>
    /// <param name="clientFactory">Injected IHttpClientFactory</param>
    public AuthService(IHttpClientFactory clientFactory) { _loginClient = clientFactory.CreateClient("Omnium_LoginClient"); }

    /// <summary>
    ///     Sends a request to /api/Token with ClientId and Secret
    ///     to get the Baerer token string
    /// </summary>
    /// <returns>Token string used for authentication</returns>
    public async Task<string> GetToken(string clientId, string clientSecret)
    {
        // TODO: Add expiration to prevent getting token for each request
        var result = await _loginClient.PostAsync($"/api/Token?clientId={clientId}&clientSecret={clientSecret}&returnAsJson=false", null);
        var content = await result.Content.ReadAsStringAsync();

        return content;
    }
}
