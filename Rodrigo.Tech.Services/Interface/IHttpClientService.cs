using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rodrigo.Tech.Services.Interface
{
    public interface IHttpClientService
    {
        /// <summary>
        ///     GET/POST/DELETE/PUT Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="httpMethod"></param>
        /// <param name="body"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> Json<T>(string url, string httpMethod, T body = default,
                                        Dictionary<string, string> headers = null);
    }
}
