using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Rodrigo.Tech.Services.Interface;
using Rodrigo.Tech.Models.Constants;

namespace Rodrigo.Tech.Azure.Functions.HttpTrigger
{
    public class LanguageHttpTrigger
    {
        private readonly ILogger _logger;
        private readonly ILanguageRepositoryService _repositoryService;

        public LanguageHttpTrigger(ILogger<LanguageHttpTrigger> logger, 
                                    ILanguageRepositoryService languageRepositoryService)
        {
            _logger = logger;
            _repositoryService = languageRepositoryService;
        }

        [FunctionName(HttpTriggerFunctionNameConstants.LANGUAGE_GETALL)]
        public async Task<IActionResult> GetAllEmails(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get",
            Route = HttpTriggerFunctionRouteConstants.LANGUAGE)] HttpRequest req
            )
        {
            try
            {
                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.LANGUAGE_GETALL} - Started");

                var result = await _repositoryService.GetItems();

                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.LANGUAGE_GETALL} - Finished");
                return new OkObjectResult(result);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{HttpTriggerFunctionNameConstants.LANGUAGE_GETALL} - Failed, Ex: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [FunctionName(HttpTriggerFunctionNameConstants.LANGUAGE_GET)]
        public async Task<IActionResult> GetEmail(
            [HttpTrigger(AuthorizationLevel.Function, "get",
            Route = HttpTriggerFunctionRouteConstants.LANGUAGE_BYID)] HttpRequest request, Guid id)
        {
            try
            {
                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.LANGUAGE_GET} - Started");

                var result = await _repositoryService.GetItem(id);

                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.LANGUAGE_GET} - Finished");
                return new OkObjectResult(result);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{HttpTriggerFunctionNameConstants.LANGUAGE_GET} - Failed, Ex: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
