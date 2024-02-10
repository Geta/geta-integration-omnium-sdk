using Geta.Integration.Omnium.Sdk;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.AddOmniumIntegration();
builder.Services.AddHostedService<SampleConnectionService>();

builder.Services.Configure<OmniumConfiguration>(o => o.ClientId = "OVERTWRITE");

var host = builder.Build();

host.Run();

public class SampleConnectionService(AuthService service, IOptions<OmniumConfiguration> configuration) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var xx = await service.GetTokenAsync(configuration.Value.ClientId, configuration.Value.ClientSecret);
    }
}
