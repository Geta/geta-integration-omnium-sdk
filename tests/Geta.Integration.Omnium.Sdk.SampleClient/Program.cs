using Geta.Integration.Omnium.Sdk;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile("appsettings.Development.json", true)
    .AddEnvironmentVariables();

builder.Services.AddOmniumIntegration();

// NOTE: if you need to run through proxy
//builder.Services.AddOmniumIntegration(() => new HttpClientHandler
//{
//    Proxy = new WebProxy("http://127.0.0.1:8080"),
//    UseProxy = true,
//    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
//});


builder.Services.AddHostedService<SampleConnectionService>();

// NOTE: supports also after post configuration
//builder.Services.Configure<OmniumConfiguration>(o => o.ClientId = "OVERTWRITE");

var host = builder.Build();

await host.RunAsync();
