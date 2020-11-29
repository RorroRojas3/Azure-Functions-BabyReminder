using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rodrigo.Tech.Services.Interface;
using Rodrigo.Tech.Models.Constants;
using Rodrigo.Tech.Models.Request;
using System.Net;

namespace Rodrigo.Tech.Azure.Functions.HttpTrigger
{
    public class EmailHttpTrigger
    {
        private readonly ILogger _logger;
        private readonly IEmailRepositoryService _repositoryService;

        public EmailHttpTrigger(ILogger<EmailHttpTrigger> logger, 
                                IEmailRepositoryService emailRepositoryService)
        {
            _logger = logger;
            _repositoryService = emailRepositoryService;
        }

        [FunctionName(HttpTriggerFunctionNameConstants.EMAIL_GETALL)]
        public async Task<IActionResult> GetAllEmails(
            [HttpTrigger(AuthorizationLevel.Function, "get",
            Route = HttpTriggerFunctionRouteConstants.EMAIL)] HttpRequest req
            )
        {
            try
            {
                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.EMAIL_GETALL} - Started");

                var result = await _repositoryService.GetItems();

                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.EMAIL_GETALL} - Finished");
                return new OkObjectResult(result);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{HttpTriggerFunctionNameConstants.EMAIL_GETALL} - Failed, Ex: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [FunctionName(HttpTriggerFunctionNameConstants.EMAIL_GET)]
        public async Task<IActionResult> GetEmail(
            [HttpTrigger(AuthorizationLevel.Function, "get",
            Route = HttpTriggerFunctionRouteConstants.EMAIL_BYID)] HttpRequest request, Guid id)
        {
            try
            {
                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.EMAIL_GET} - Started");

                var result = await _repositoryService.GetItem(id);

                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.EMAIL_GET} - Finished");
                return new OkObjectResult(result);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{HttpTriggerFunctionNameConstants.EMAIL_GET} - Failed, Ex: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [FunctionName(HttpTriggerFunctionNameConstants.EMAIL_POST)]
        public async Task<IActionResult> PostEmail(
            [HttpTrigger(AuthorizationLevel.Function, "post",
            Route = HttpTriggerFunctionRouteConstants.EMAIL)] HttpRequest request)
        {
            try
            {
                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.EMAIL_POST} - Started");

                var input = await request.ReadAsStringAsync();
                var emailBodyRequest = JsonConvert.DeserializeObject<EmailRequest>(input);
                var result = await _repositoryService.PostItem(emailBodyRequest);

                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.EMAIL_POST} - Finished");
                return new StatusCodeResult(StatusCodes.Status201Created);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{HttpTriggerFunctionNameConstants.EMAIL_POST} - Failed, Ex: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [FunctionName(HttpTriggerFunctionNameConstants.EMAIL_PUT)]
        public async Task<IActionResult> PutEmail(
            [HttpTrigger(AuthorizationLevel.Function, "put",
            Route = HttpTriggerFunctionRouteConstants.EMAIL_BYID)] HttpRequest request, Guid id)
        {
            try
            {
                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.EMAIL_PUT} - Started");

                var input = await request.ReadAsStringAsync();
                var emailBodyRequest = JsonConvert.DeserializeObject<EmailRequest>(input);
                var result = await _repositoryService.PutItem(id, emailBodyRequest);

                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.EMAIL_PUT} - Finished");
                return new OkObjectResult(result);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{HttpTriggerFunctionNameConstants.EMAIL_PUT} - Failed, Ex: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [FunctionName(HttpTriggerFunctionNameConstants.EMAIL_DELETE)]
        public async Task<IActionResult> DeleteEmail(
            [HttpTrigger(AuthorizationLevel.Function, "delete",
            Route = HttpTriggerFunctionRouteConstants.EMAIL_BYID)] HttpRequest request, Guid id)
        {
            try
            {
                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.EMAIL_DELETE} - Started");

                var result = await _repositoryService.DeleteItem(id);

                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.EMAIL_DELETE} - Finished");
                return new OkObjectResult(result);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{HttpTriggerFunctionNameConstants.EMAIL_DELETE} - Failed, Ex: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
