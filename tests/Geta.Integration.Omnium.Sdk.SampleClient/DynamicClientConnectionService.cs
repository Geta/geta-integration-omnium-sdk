using Geta.Integration.Omnium.Sdk;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

public class DynamicClientConnectionService(IServiceProvider sp) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var t1 = GetHealthForTenant("Sprell-Test",
                                    Options.Create(new OmniumConfiguration
                                    {
                                        BaseAddress = "https://apitest.omnium.no",
                                        ClientId = "geta-c34d9e2d-06aa-4285-a4cf-ee0f38da791b",
                                        ClientSecret = "602a30ad265b4f9f92949032b1bd2801-67102b350c6642a49139b016a673f203"
                                    }),
                                    stoppingToken);

        var t2 = GetHealthForTenant("Omnium-Test",
                                    Options.Create(new OmniumConfiguration
                                    {
                                        BaseAddress = "https://apitest.omnium.no",
                                        ClientId = "valdis-multi-tenancy-test-6d9f5c8e-af4c-46d1-ada6-ab569052598c",
                                        ClientSecret = "a586c59f730c4bd3ac7ca06bb5214d89-4132380779ec42709540fffbd2eae9c5"
                                    }),
                                    stoppingToken);

        await Task.WhenAll(t1, t2);
    }

    private async Task GetHealthForTenant(string tenantId, IOptions<OmniumConfiguration> config, CancellationToken stoppingToken)
    {
        var scope = sp.CreateScope();

        var clientFactory = scope.ServiceProvider.GetRequiredService<IOmniumClientFactory>();

        var client = clientFactory.CreateClient(tenantId, config);
        var health = await client!.HealthHealthAsync(stoppingToken);

        Console.WriteLine($"{tenantId}: {health?.StatusCode}");
    }
}
