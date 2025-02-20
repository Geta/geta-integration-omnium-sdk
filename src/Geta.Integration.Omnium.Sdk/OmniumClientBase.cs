using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Geta.Integration.Omnium.Sdk;

public partial class OmniumClientBase
{
    private static JsonSerializerSettings ConfigureJsonSerializerSettings(JsonSerializerSettings settings)
    {
        settings.DefaultValueHandling = DefaultValueHandling.Ignore;
        settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

        return settings;
    }
}
