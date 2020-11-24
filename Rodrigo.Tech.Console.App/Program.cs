using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
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

            var httpClientService = serviceProvider.GetService<IHttpClientService>();
            var url = "https://www.google.com";
            var response = await httpClientService.Json<object>(url, HttpMethods.Get);
            var responseContent = await response.Content.ReadAsStringAsync();
        }
    }
}
