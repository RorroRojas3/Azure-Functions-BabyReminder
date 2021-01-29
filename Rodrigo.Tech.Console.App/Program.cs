using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rodrigo.Tech.ConsoleApp.Model.Constants;
using Rodrigo.Tech.Services.Implementation;
using Rodrigo.Tech.Services.Interface;
using System;
using System.Threading.Tasks;

namespace Rodrigo.Tech.ConsoleApp
{
    public class Program
    {
        public static async Task Main()
        {
            var serviceProvider = new ServiceCollection()
            .AddLogging()
            .AddTransient<IHttpClientService, HttpClientService>()
            .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>()
            .CreateLogger<Program>();
            logger.LogInformation($"{nameof(Program)} - {nameof(Main)} - Started");

            var httpClientService = serviceProvider.GetService<IHttpClientService>();
            var url = Environment.GetEnvironmentVariable(EnvironmentVariableConstants.BABYREMINDER_URL);
            url = string.Format(url, Environment.GetEnvironmentVariable(EnvironmentVariableConstants.BABYREMINDER_KEY));

            logger.LogInformation($"{nameof(Program)} - {nameof(Main)} - Calling {url}");
            var response = await httpClientService.Json<object>(url, HttpMethods.Post);

            var responseContent = await response.Content.ReadAsStringAsync();
            logger.LogInformation($"{nameof(Program)} - {nameof(Main)} - Response: {responseContent}");

            if (!response.IsSuccessStatusCode)
            {
                logger.LogError($"{nameof(Program)} - {nameof(Main)} - Unsuccesfull call to {url}, " +
                    $"Status: {response.StatusCode}, Response: {responseContent}");
            }

            logger.LogInformation($"{nameof(Program)} - {nameof(Main)} - Finished");
        }
    }
}
