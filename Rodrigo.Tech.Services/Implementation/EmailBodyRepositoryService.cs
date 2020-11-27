using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rodrigo.Tech.Models.Request;
using Rodrigo.Tech.Models.Response;
using Rodrigo.Tech.Repository.Pattern.Interface;
using Rodrigo.Tech.Repository.Tables;
using Rodrigo.Tech.Services.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Rodrigo.Tech.Services.Implementation
{
    public class EmailBodyRepositoryService : IEmailBodyRepositoryService
    {
        private readonly IRepositoryPattern<EmailBody> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public EmailBodyRepositoryService(IRepositoryPattern<EmailBody> repository,
                                        IMapper mapper,
                                        ILogger<EmailBodyRepositoryService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteItem(Guid id)
        {
            _logger.LogInformation($"{nameof(EmailBodyRepositoryService)} " +
                $"- {nameof(DeleteItem)} - Started, Id: {id}");
            return await _repository.Delete(id);
        }

        /// <inheritdoc/>
        public async Task<EmailBodyFileResponse> GetItem(Guid id)
        {
            _logger.LogInformation($"{nameof(EmailBodyRepositoryService)} " +
                $"- {nameof(GetItem)} - Started, Id: {id}");

            var item = await _repository.Get(id);

            if (item == null)
            {
                _logger.LogError($"{nameof(EmailBodyRepositoryService)} " +
                $"- {nameof(GetItem)} - Not found, Id: {id}");
                throw new KeyNotFoundException();
            }

            var response = new EmailBodyFileResponse
            {
                File = new MemoryStream(item.File),
                FileName = item.Name
            };

            _logger.LogInformation($"{nameof(EmailBodyRepositoryService)} " +
                $"- {nameof(GetItem)} - Finished, Id: {id}");
            return response;
        }

        /// <inheritdoc/>
        public async Task<List<EmailBodyResponse>> GetItems()
        {
            _logger.LogInformation($"{nameof(EmailBodyRepositoryService)} " +
                $"- {nameof(GetItems)} - Started");
            var items = await _repository.GetAll();

            if (items.Count == 0)
            {
                _logger.LogInformation($"{nameof(EmailBodyRepositoryService)} " +
                $"- {nameof(GetItems)} - Not found");
                return null;
            }

            _logger.LogInformation($"{nameof(EmailBodyRepositoryService)} " +
                $"- {nameof(GetItems)} - Finished");
            return _mapper.Map<List<EmailBodyResponse>>(items);
        }

        /// <inheritdoc/>
        public async Task<EmailBodyResponse> PostItem(Guid languageId, IFormFile formFile)
        {
            _logger.LogInformation($"{nameof(EmailBodyRepositoryService)} " +
                $"- {nameof(PostItem)} - Started, LanguageId: {languageId}");

            if (formFile == null)
            {
                _logger.LogError($"{nameof(EmailBodyRepositoryService)} - Formfile is null");
                throw new ArgumentNullException();
            }

            var newItem = new EmailBody
            {
                LanguageId = languageId,
                Name = formFile.FileName,
                File = await FormFileToByteArray(formFile)
            };

            var addedItem = await _repository.Add(newItem);

            _logger.LogInformation($"{nameof(EmailBodyRepositoryService)} " +
                $"- {nameof(PostItem)} - Finished, LanguageId: {languageId}");
            return _mapper.Map<EmailBodyResponse>(addedItem);
        }

        /// <inheritdoc/>
        public async Task<EmailBodyResponse> PutItem(Guid id, IFormFile formFile)
        {
            _logger.LogInformation($"{nameof(EmailBodyRepositoryService)} " +
                $"- {nameof(PutItem)} - Started, Id: {id}");

            if (formFile == null)
            {
                _logger.LogInformation($"{nameof(EmailBodyRepositoryService)} " +
                $"- {nameof(PutItem)} - Formfile is null, Id: {id}");
                throw new ArgumentException();
            }

            var item = await _repository.Get(id);

            if (item == null)
            {
                _logger.LogError($"{nameof(EmailBodyRepositoryService)} " +
                $"- {nameof(PutItem)} - Not found");
                throw new KeyNotFoundException();
            }

            item.File = await FormFileToByteArray(formFile);
            item.Name = formFile.FileName;
            await _repository.Update(item);

            _logger.LogInformation($"{nameof(EmailBodyRepositoryService)} " +
                $"- {nameof(PutItem)} - Finished, Id: {id}");
            return _mapper.Map<EmailBodyResponse>(item);
        }

        private async Task<byte[]> FormFileToByteArray(IFormFile stream)
        {
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
