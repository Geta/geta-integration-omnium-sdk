using Geta.Integration.Omnium.Sdk;
using Microsoft.Extensions.Hosting;

public class SampleConnectionService(IClient client) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var orderSettings = await client.SettingsOrderSettingsAsync(stoppingToken);
        
        var health = await client.HealthHealthAsync(stoppingToken);
    }
}
