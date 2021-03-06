﻿using Microsoft.AspNetCore.Http;
using Rodrigo.Tech.
    Models.Request;
using Rodrigo.Tech.Models.Response;
using Rodrigo.Tech.Repository.Tables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Rodrigo.Tech.Services.Interface
{
    public interface IEmailBodyRepositoryService
    {
        /// <summary>
        ///     Gets all items from DB
        /// </summary>
        /// <returns></returns>
        Task<List<EmailBodyResponse>> GetItems();

        /// <summary>
        ///     Gets item from DB based on Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EmailBodyFileResponse> GetItem(Guid id);

        /// <summary>
        ///     Gets T with LINQ expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        EmailBodyFileResponse GetItemWithExpression(Func<EmailBody, bool> predicate);

        /// <summary>
        ///     Gets List of items with LINQ expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<EmailBodyResponse> GetAllItemsWithExpression(Func<EmailBody, bool> predicate);

        /// <summary>
        ///     Creates item on DB
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<EmailBodyResponse> PostItem(Guid languageId, IFormFile formFile);

        /// <summary>
        ///     Updates item on DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<EmailBodyResponse> PutItem(Guid id, IFormFile formFile);

        /// <summary>
        ///     Deletes item from DB based on Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteItem(Guid id);
    }
}
