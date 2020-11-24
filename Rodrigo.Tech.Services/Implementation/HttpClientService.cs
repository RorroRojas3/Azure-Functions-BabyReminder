using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rodrigo.Tech.Services.Interface;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rodrigo.Tech.Services.Implementation
{
    public class HttpClientService : IHttpClientService
    {
        private readonly ILogger _logger;

        public HttpClientService(ILogger<HttpClientService> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> Json<T>(string url, string httpMethod, T body = default,
                                                Dictionary<string, string> headers = null)
        {
            _logger.LogInformation($"HttpClientService - Json - Started, Url: {url}, " +
                $"Body: {JsonConvert.SerializeObject(body)}, HttpMethod: {httpMethod}");

            using var client = CreateClient();
            HttpResponseMessage httpResponseMessage = null;
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }

            switch (httpMethod.ToUpper())
            {
                case "GET":
                    httpResponseMessage = await client.GetAsync(url);
                    break;
                case "POST":
                    httpResponseMessage = await client.PostAsJsonAsync(url, body);
                    break;
                case "PUT":
                    httpResponseMessage = await client.PutAsJsonAsync(url, body);
                    break;
                case "DELETE":
                    httpResponseMessage = await client.DeleteAsync(url);
                    break;
                default:
                    _logger.LogError($"HttpClientService - Json - Invalid HttpMethod");
                    break;
            }

            return httpResponseMessage;
        }

        /// <summary>
        ///     Creates HttpClient 
        /// </summary>
        /// <returns></returns>
        private HttpClient CreateClient()
        {
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                {
                    return true;
                }
            };
            var httpClient = new HttpClient(httpClientHandler);
            return httpClient;
        }
    }
}
