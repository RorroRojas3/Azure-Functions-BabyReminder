using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rodrigo.Tech.Models.Request;
using Rodrigo.Tech.Models.Response;
using Rodrigo.Tech.Repository.Pattern.Interface;
using Rodrigo.Tech.Repository.Tables;
using Rodrigo.Tech.Services.Interface;
using System;
using System.Collections.Generic;
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
        public async Task<EmailBodyResponse> GetItem(Guid id)
        {
            _logger.LogInformation($"{nameof(EmailBodyRepositoryService)} " +
                $"- {nameof(GetItem)} - Started, Id: {id}");

            var item = await _repository.Get(id);

            if (item == null)
            {
                _logger.LogError($"{nameof(EmailBodyRepositoryService)} " +
                $"- {nameof(GetItem)} - Not found, Id: {id}");
                return null;
            }

            _logger.LogInformation($"{nameof(EmailBodyRepositoryService)} " +
                $"- {nameof(GetItem)} - Finished, Id: {id}");
            return _mapper.Map<EmailBodyResponse>(item);
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
                $"- {nameof(GetItems)} - No emailBodies found");
                return null;
            }

            _logger.LogInformation($"{nameof(EmailBodyRepositoryService)} " +
                $"- {nameof(GetItems)} - Finished");
            return _mapper.Map<List<EmailBodyResponse>>(items);
        }

        /// <inheritdoc/>
        public async Task<EmailBodyResponse> PostItem(EmailBodyRequest request)
        {
            _logger.LogInformation($"{nameof(EmailBodyRepositoryService)} " +
                $"- {nameof(PostItem)} - Started, Request: {JsonConvert.SerializeObject(request)}");
            var newItem = _mapper.Map<EmailBody>(request);

            var addedItem = await _repository.Add(newItem);

            _logger.LogInformation($"{nameof(EmailBodyRepositoryService)} " +
                $"- {nameof(PostItem)} - Finished, Request: {JsonConvert.SerializeObject(request)}");
            return _mapper.Map<EmailBodyResponse>(addedItem);
        }

        /// <inheritdoc/>
        public async Task<EmailBodyResponse> PutItem(Guid id, EmailBodyRequest request)
        {
            _logger.LogInformation($"{nameof(EmailBodyRepositoryService)} " +
                $"- {nameof(PutItem)} - Started, Request: {JsonConvert.SerializeObject(request)}");
            var item = await _repository.Get(id);

            if (item == null)
            {
                _logger.LogInformation($"{nameof(EmailBodyRepositoryService)} " +
                $"- {nameof(PutItem)} - Not found, Request: {JsonConvert.SerializeObject(request)}");
                return null;
            }

            _mapper.Map(request, item);
            await _repository.Update(item);

            _logger.LogInformation($"{nameof(EmailBodyRepositoryService)} " +
                $"- {nameof(PutItem)} - Finished, Request: {JsonConvert.SerializeObject(request)}");
            return _mapper.Map<EmailBodyResponse>(item);
        }
    }
}
