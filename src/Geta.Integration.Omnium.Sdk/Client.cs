namespace Geta.Integration.Omnium.Sdk;

internal class Client : OmniumClientBase, IClient
{
    public Client(HttpClient httpClient) : base(httpClient)
    {
    }

    protected override async Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(
        HttpResponseMessage response,
        IReadOnlyDictionary<string, IEnumerable<string>> headers,
        CancellationToken cancellationToken)
    {
        if (typeof(T) == typeof(string))
        {
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            return new ObjectResponseResult<T>((T)(object)content, content);
        }

        return await base.ReadObjectResponseAsync<T>(response, headers, cancellationToken);
    }
}
