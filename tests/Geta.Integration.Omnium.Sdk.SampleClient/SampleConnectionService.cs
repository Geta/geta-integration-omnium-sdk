using Geta.Integration.Omnium.Sdk;
using Microsoft.Extensions.Hosting;

public class SampleConnectionService(Client client) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var orderSettings = await client.SettingsOrderSettingsAsync(stoppingToken);

        await client.HealthHealthAsync(stoppingToken);
    }
}
