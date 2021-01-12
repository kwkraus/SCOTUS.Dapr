using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dapr.DotNetCore.WindowsService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FilingsController : ControllerBase
    {
        /// <summary>
        /// State store name.
        /// </summary>
        public const string StoreName = "statestore";
        private readonly ILogger<FilingsController> _logger;

        public FilingsController(ILogger<FilingsController> logger)
        {
            _logger = logger;
        }

        [Topic("pubsub", "filingdata")]
        [HttpPost("processfiling")]
        public async Task<ActionResult<Filing>> ProcessFiling(Filing newFiling, [FromServices] DaprClient daprClient)
        {
            _logger.LogInformation($"Enter ProcessFiling for filing {JsonSerializer.Serialize<Filing>(newFiling)}");
            var state = await daprClient.GetStateEntryAsync<Filing>(StoreName, newFiling.Id);
            state.Value ??= newFiling;
            await state.SaveAsync();
            return state.Value;
        }
    }
}
