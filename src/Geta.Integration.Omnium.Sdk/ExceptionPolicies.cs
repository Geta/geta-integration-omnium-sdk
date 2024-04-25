using System.Net;
using Polly;

namespace Geta.Integration.Omnium.Sdk;

/// <summary>
/// Exception policies
/// </summary>
public static class ExceptionPolicies
{
    /// <summary>
    /// Retry on too many requests policy
    /// </summary>
    /// <returns></returns>
    public static IAsyncPolicy<HttpResponseMessage> GetRetryOnTooManyRequestsPolicy()
    {
        return Policy<HttpResponseMessage>
            .Handle<HttpRequestException>(ex => ex.StatusCode == HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}
