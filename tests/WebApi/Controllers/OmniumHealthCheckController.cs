using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Geta.Integration.Omnium.Sdk.Tests.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OmniumHealthCheckController : ControllerBase
{
    private readonly ILogger<OmniumHealthCheckController> _logger;
    private readonly IMediator _sender;

    public OmniumHealthCheckController(ILogger<OmniumHealthCheckController> logger, IMediator sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpGet]
    public IActionResult Get()
    {
        _sender.Send(new CheckOmniumHealth.Query());
        return Ok();
    }
}

public abstract class CheckOmniumHealth
{
    public class Handler : IRequestHandler<Query, string>
    {
        private readonly IOmniumClientFactory _factory;

        public Handler(IOmniumClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<string> Handle(Query request, CancellationToken cancellationToken)
        {
            var c = _factory.CreateClient("TestTenant", Options.Create(new OmniumConfiguration
            {
                BaseAddress = "https://apitest.omnium.no",
                ClientId = "valdis-multi-tenancy-test-6d9f5c8e-af4c-46d1-ada6-ab569052598c",
                ClientSecret = "a586c59f730c4bd3ac7ca06bb5214d89-4132380779ec42709540fffbd2eae9c5"
            }));

            if (c == null)
            {
                throw new InvalidOperationException("Failed to create Omnium client.");
            }

            var result = await c.HealthHealthAsync(cancellationToken);

            return result?.StatusCode.ToString()!;
        }
    }

    public class Query : IRequest<string>
    {
    }
}
