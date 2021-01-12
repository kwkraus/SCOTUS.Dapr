using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Dapr.DotNetCore.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FilingsController : ControllerBase
    {
        private readonly ILogger<FilingsController> _logger;
        private readonly string _pubsubName = "pubsub";
        private readonly string _topicName = "filingdata";

        public FilingsController(ILogger<FilingsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> ProcessFiling(Filing filing, [FromServices] DaprClient client)
        {
            _logger.LogInformation("Processing new filing");

            await client.PublishEventAsync<Filing>(_pubsubName, _topicName, filing);

            return Ok();
        }
    }
}
