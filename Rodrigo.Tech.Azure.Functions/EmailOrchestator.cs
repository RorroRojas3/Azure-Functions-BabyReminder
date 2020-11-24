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
          
        [FunctionName(FunctionNameConstants.EMAIL_HTTPSTART)]
        public async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter)
        {
            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync(FunctionNameConstants.EMAIL_ORCHESTRATOR, null);

            _logger.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }


        [FunctionName(FunctionNameConstants.EMAIL_ORCHESTRATOR)]
        public async Task RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            await context.CallActivityAsync(FunctionNameConstants.EMAIL_SEND, context.InstanceId);
        }

        [FunctionName(FunctionNameConstants.EMAIL_SEND)]
        public async Task EmailSend([ActivityTrigger] string instanceId)
        {
            _logger.LogInformation($"{FunctionNameConstants.EMAIL_SEND} - Started");
            await _stmpService.SendEmail();
            _logger.LogInformation($"{FunctionNameConstants.EMAIL_SEND} - Finished");
        }
    }
}