using Rodrigo.Tech.Models.Request;
using Rodrigo.Tech.Models.Response;
using Rodrigo.Tech.Repository.Pattern.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rodrigo.Tech.Services.Interface
{
    public interface IEmailRepositoryService
    {
        /// <summary>
        ///     Gets all items from DB
        /// </summary>
        /// <returns></returns>
        Task<List<EmailResponse>> GetItems();

        /// <summary>
        ///     Gets item from DB based on Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EmailResponse> GetItem(Guid id);

        /// <summary>
        ///     Creates item on DB
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<EmailResponse> PostItem(EmailRequest request);

        /// <summary>
        ///     Updates item on DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<EmailResponse> PutItem(Guid id, EmailRequest request);

        /// <summary>
        ///     Deletes item from DB based on Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteItem(Guid id);
    }
}
