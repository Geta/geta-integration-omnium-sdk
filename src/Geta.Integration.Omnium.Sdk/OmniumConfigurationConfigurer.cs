using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Geta.Integration.Omnium.Sdk;

internal class OmniumConfigurationConfigurer : IConfigureOptions<OmniumConfiguration>
{
    private readonly IConfiguration _configuration;

    public OmniumConfigurationConfigurer(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(OmniumConfiguration options)
    {
        _configuration.Bind("Omnium", options);
    }
}
