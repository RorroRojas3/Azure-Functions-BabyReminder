using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using net_core_baby_reminder.Services.Interface;

namespace net_core_baby_reminder
{
    public class SendEmailFunction
    {
        private readonly ILogger _logger;
        private readonly ISendEmailService _sendEmailService;

        public SendEmailFunction(ILogger<SendEmailFunction> logger, ISendEmailService sendEmailService)
        {
            _logger = logger;
            _sendEmailService = sendEmailService;
        }

        [FunctionName("Function1")]
        public void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            _logger.LogInformation($"SendEmailFunction - Started at {DateTime.UtcNow}");

            _sendEmailService.SendEmail();
        }
    }
}
