using System.ComponentModel.DataAnnotations;

namespace Geta.Integration.Omnium.Sdk;

public class OmniumConfiguration
{
    [Required]
    public string BaseAddress { get; set; }

    [Required]
    public string ClientId { get; set; }

    [Required]
    public string ClientSecret { get; set; }
}
