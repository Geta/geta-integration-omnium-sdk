using Microsoft.Extensions.Options;

namespace Geta.Integration.Omnium.Sdk;

internal class OmniumConfigurationValidator : IValidateOptions<OmniumConfiguration>
{
    public ValidateOptionsResult Validate(string name, OmniumConfiguration options)
    {
        var x = new DataAnnotationValidateOptions<OmniumConfiguration>(name);

        return x.Validate(name, options);
    }
}
