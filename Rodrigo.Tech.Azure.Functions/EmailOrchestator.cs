using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
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
          
        [FunctionName("EmailOrchestrator_HttpStart")]
        public async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("Function1", null);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }


        [FunctionName("Function1")]
        public async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var outputs = new List<string>
            {
                await context.CallActivityAsync<string>("Function1_Hello", "Tokyo"),
                await context.CallActivityAsync<string>("Function1_Hello", "Seattle"),
                await context.CallActivityAsync<string>("Function1_Hello", "London")
            };

            return outputs;
        }

        [FunctionName("Function1_Hello")]
        public string SayHello([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($"Saying hello to {name}.");
            return $"Hello {name}!";
        }
    }
}