using Geta.Integration.Omnium.Sdk;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

public class SampleConnectionService(IClient client, IOptions<OmniumConfiguration> config) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //var orderSettings = await client.SettingsOrderSettingsAsync(stoppingToken);

        //var health = await client.HealthHealthAsync(stoppingToken);

        //var paymentOptions = await client.CartGetPaymentOptionsAsync("1234", stoppingToken);

        var dr = client.PricesPutPricesAsync(
            new List<OmniumPriceUpdateRequest>
            {
                new()
                {
                    Prices = new List<OmniumPrice>
                    {
                        new()
                        {
                            MarketId = "4000",
                            CurrencyCode = "NOK",
                            Code = "65533",
                            ProductId = "P736183"
                        }
                    }
                }
            },
            stoppingToken);
    }
}
