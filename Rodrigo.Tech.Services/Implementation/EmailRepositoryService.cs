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
    public class EmailRepositoryService : IEmailRepositoryService
    {
        private readonly IRepositoryPattern<Email> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public EmailRepositoryService(IRepositoryPattern<Email> repository,
                                        IMapper mapper,
                                        ILogger<EmailRepositoryService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteItem(Guid id)
        {
            _logger.LogInformation($"{nameof(EmailRepositoryService)} " +
                $"- {nameof(DeleteItem)} - Started, Id: {id}");
            return await _repository.Delete(id);
        }

        /// <inheritdoc/>
        public async Task<EmailResponse> GetItem(Guid id)
        {
            _logger.LogInformation($"{nameof(EmailRepositoryService)} " +
                $"- {nameof(GetItem)} - Started, Id: {id}");

            var item = await _repository.Get(id);

            if (item == null)
            {
                _logger.LogError($"{nameof(EmailRepositoryService)} " +
                $"- {nameof(GetItem)} - Not found, Id: {id}");
                throw new KeyNotFoundException();
            }

            _logger.LogInformation($"{nameof(EmailRepositoryService)} " +
                $"- {nameof(GetItem)} - Finished, Id: {id}");
            return _mapper.Map<EmailResponse>(item);
        }

        /// <inheritdoc/>
        public async Task<List<EmailResponse>> GetItems()
        {
            _logger.LogInformation($"{nameof(EmailRepositoryService)} " +
                $"- {nameof(GetItems)} - Started");
            var items = await _repository.GetAll();

            if (items.Count == 0)
            {
                _logger.LogInformation($"{nameof(EmailRepositoryService)} " +
                $"- {nameof(GetItems)} - Not found");
                throw new KeyNotFoundException();
            }

            _logger.LogInformation($"{nameof(EmailRepositoryService)} " +
                $"- {nameof(GetItems)} - Finished");
            return _mapper.Map<List<EmailResponse>>(items);
        }

        /// <inheritdoc/>
        public async Task<EmailResponse> PostItem(EmailRequest request)
        {
            _logger.LogInformation($"{nameof(EmailRepositoryService)} " +
                $"- {nameof(PostItem)} - Started, Request: {JsonConvert.SerializeObject(request)}");
            var newItem = _mapper.Map<Email>(request);

            var addedItem = await _repository.Add(newItem);

            _logger.LogInformation($"{nameof(EmailRepositoryService)} " +
                $"- {nameof(PostItem)} - Finished, Request: {JsonConvert.SerializeObject(request)}");
            return _mapper.Map<EmailResponse>(addedItem);
        }

        /// <inheritdoc/>
        public async Task<EmailResponse> PutItem(Guid id, EmailRequest request)
        {
            _logger.LogInformation($"{nameof(EmailRepositoryService)} " +
                $"- {nameof(PutItem)} - Started, Request: {JsonConvert.SerializeObject(request)}");
            var item = await _repository.Get(id);

            if (item == null)
            {
                _logger.LogInformation($"{nameof(EmailRepositoryService)} " +
                $"- {nameof(PutItem)} - Not found, Request: {JsonConvert.SerializeObject(request)}");
                throw new KeyNotFoundException();
            }

            _mapper.Map(request, item);
            await _repository.Update(item);

            _logger.LogInformation($"{nameof(EmailRepositoryService)} " +
                $"- {nameof(PutItem)} - Finished, Request: {JsonConvert.SerializeObject(request)}");
            return _mapper.Map<EmailResponse>(item);
        }
    }
}
