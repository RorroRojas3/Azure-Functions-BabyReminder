using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Rodrigo.Tech.Models.Constants;
using Rodrigo.Tech.Services.Interface;

namespace Rodrigo.Tech.Azure.Functions
{
    public class EmailOrchestator
    {
        private readonly ILogger _logger;
        private readonly IStmpService _stmpService;

        public EmailOrchestator(ILogger<EmailOrchestator> logger, IStmpService stmpService)
        {
            _logger = logger;
            _stmpService = stmpService;
        }
          
        [FunctionName(AzureFunctionsConstants.EMAIL_HTTPSTART_FUNCTION)]
        public async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter)
        {
            string instanceId = await starter.StartNewAsync(AzureFunctionsConstants.EMAIL_ORCHESTRATOR_FUNCTION, null);

            _logger.LogInformation($"Started orchestration with ID {instanceId}");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }


        [FunctionName(AzureFunctionsConstants.EMAIL_ORCHESTRATOR_FUNCTION)]
        public async Task RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            await context.CallActivityAsync(AzureFunctionsConstants.EMAIL_SEND_FUNCTION, context.InstanceId);
        }

        [FunctionName(AzureFunctionsConstants.EMAIL_SEND_FUNCTION)]
        public async Task SendEmail([ActivityTrigger] string instanceId)
        {
            _logger.LogInformation($"{AzureFunctionsConstants.EMAIL_SEND_FUNCTION} - Started");
            await _stmpService.SendEmail();
            _logger.LogInformation($"{AzureFunctionsConstants.EMAIL_SEND_FUNCTION} - Finished");
        }
    }
}