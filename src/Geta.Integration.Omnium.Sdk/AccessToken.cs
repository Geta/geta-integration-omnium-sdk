namespace Geta.Integration.Omnium.Sdk;

internal readonly struct AccessToken
{
    public string Scheme { get; }
    public string Token { get; }

    public AccessToken(string scheme, string accessToken)
    {
        Scheme = scheme;
        Token = accessToken;
    }
}
