using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rodrigo.Tech.Services.Implementation;
using Rodrigo.Tech.Services.Interface;
using System.Threading.Tasks;

namespace Rodrigo.Tech.ConsoleApp
{
    public class Program
    {
        public static async Task Main()
        {
            var serviceProvider = new ServiceCollection()
            .AddTransient<IHttpClientService, HttpClientService>()
            .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>()
            .CreateLogger<Program>();
            logger.LogInformation($"ConsoleApp - Baby reminder - Started");

            var httpClientService = serviceProvider.GetService<IHttpClientService>();
            var url = "https://www.google.com";

            logger.LogInformation($"ConsoleApp - Baby reminder - Calling {url}");
            var response = await httpClientService.Json<object>(url, HttpMethods.Get);

            logger.LogInformation($"ConsoleApp - Baby reminder - Reading response content");
            var responseContent = await response.Content.ReadAsStringAsync();
            logger.LogInformation($"ConsoleApp - Baby reminder - Response: {responseContent}");

            if (!response.IsSuccessStatusCode)
            {
                logger.LogError($"ConsoleApp - BabyReminder - Unsuccesfull call to {url}, " +
                    $"Status: {response.StatusCode}, Response: {responseContent}");
            }

            logger.LogInformation($"ConsoleApp - Baby reminder - Finished");
        }
    }
}
