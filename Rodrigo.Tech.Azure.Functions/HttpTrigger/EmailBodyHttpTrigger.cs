using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Rodrigo.Tech.Models.Constants;
using Rodrigo.Tech.Models.Request;
using Rodrigo.Tech.Services.Interface;
using System;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http.Headers;

namespace Rodrigo.Tech.Azure.Functions.HttpTrigger
{
    public class EmailBodyHttpTrigger
    {
        private readonly ILogger _logger;
        private readonly IEmailBodyRepositoryService _repositoryService;

        public EmailBodyHttpTrigger(ILogger<EmailBodyHttpTrigger> logger,
                                    IEmailBodyRepositoryService emailBodyRepositoryService)
        {
            _logger = logger;
            _repositoryService = emailBodyRepositoryService;

        }

        [FunctionName(HttpTriggerFunctionNameConstants.EMAILBODY_GETALL)]
        public async Task<IActionResult> GetAllEmailBodies(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", 
            Route = HttpTriggerFunctionRouteConstants.EMAILBODY)] HttpRequest req
            )
        {
            try
            {
                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.EMAILBODY_GETALL} - Started");

                var result = await _repositoryService.GetItems();

                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.EMAILBODY_GETALL} - Finished");
                return new OkObjectResult(result);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{HttpTriggerFunctionNameConstants.EMAILBODY_GETALL} - Failed, Ex: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [FunctionName(HttpTriggerFunctionNameConstants.EMAILBODY_GET)]
        public async Task<IActionResult> GetEmailBody(
            [HttpTrigger(AuthorizationLevel.Function, "get", 
            Route = HttpTriggerFunctionRouteConstants.EMAILBODY_BYID)] HttpRequest request, Guid id)
        {
            try
            {
                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.EMAILBODY_GETALL} - Started");

                var result = await _repositoryService.GetItem(id);

                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.EMAILBODY_GETALL} - Finished");
                return new FileStreamResult(result, "application/octet-stream");
            }
            catch(Exception ex)
            {
                _logger.LogError($"{HttpTriggerFunctionNameConstants.EMAILBODY_GETALL} - Failed, Ex: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [FunctionName(HttpTriggerFunctionNameConstants.EMAILBODY_POST)]
        public async Task<IActionResult> PostEmailBody(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", 
            Route = HttpTriggerFunctionRouteConstants.EMAILBODY_LANGUAGEID)] HttpRequest request, Guid languageId)
        {
            try
            {
                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.EMAILBODY_POST} - Started");

                var input = await request.ReadFormAsync();
                var formFile = input.Files.FirstOrDefault();
                var result = await _repositoryService.PostItem(languageId, formFile);

                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.EMAILBODY_POST} - Finished");
                return new StatusCodeResult(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{HttpTriggerFunctionNameConstants.EMAILBODY_POST} - Failed, Ex: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [FunctionName(HttpTriggerFunctionNameConstants.EMAILBODY_PUT)]
        public async Task<IActionResult> PutEmailBody(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", 
            Route = HttpTriggerFunctionRouteConstants.EMAILBODY_BYID)] HttpRequest request, Guid id)
        {
            try
            {
                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.EMAILBODY_PUT} - Started");

                var input = await request.ReadFormAsync();
                var formFile = input.Files.FirstOrDefault();
                var result = await _repositoryService.PutItem(id, formFile);

                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.EMAILBODY_PUT} - Finished");
                return new OkObjectResult(result);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{HttpTriggerFunctionNameConstants.EMAILBODY_PUT} - Failed, Ex: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [FunctionName(HttpTriggerFunctionNameConstants.EMAILBODY_DELETE)]
        public async Task<IActionResult> DeleteEmailBody(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", 
            Route = HttpTriggerFunctionRouteConstants.EMAILBODY_BYID)] HttpRequest request, Guid id)
        {
            try
            {
                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.EMAILBODY_DELETE} - Started");

                var result = await _repositoryService.DeleteItem(id);

                _logger.LogInformation($"{HttpTriggerFunctionNameConstants.EMAILBODY_DELETE} - Finished");
                return new OkObjectResult(result);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{HttpTriggerFunctionNameConstants.EMAILBODY_DELETE} - Failed, Ex: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
