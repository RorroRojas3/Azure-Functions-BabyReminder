using AutoMapper;
using Microsoft.Extensions.Logging;
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
        private readonly ILanguageRepositoryService _languageRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public EmailBodyRepositoryService(IRepositoryPattern<EmailBody> repository,
                                        IMapper mapper,
                                        ILanguageRepositoryService languageRepositoryService,
                                        ILogger<EmailBodyRepositoryService> logger)
        {
            _repository = repository;
            _languageRepository = languageRepositoryService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteItem(Guid id)
        {
            return await _repository.Delete(id);
        }

        /// <inheritdoc/>
        public async Task<EmailBodyResponse> GetItem(Guid id)
        {
            var item = await _repository.Get(id);

            if (item == null)
            {
                return null;
            }

            return _mapper.Map<EmailBodyResponse>(item);
        }

        /// <inheritdoc/>
        public async Task<List<EmailBodyResponse>> GetItems()
        {
            var items = await _repository.GetAll();

            if (items == null)
            {
                return null;
            }

            return _mapper.Map<List<EmailBodyResponse>>(items);
        }

        /// <inheritdoc/>
        public async Task<EmailBodyResponse> PostItem(EmailBodyRequest item)
        {
            var newItem = _mapper.Map<EmailBody>(item);

            var addedItem = await _repository.Add(newItem);

            return _mapper.Map<EmailBodyResponse>(addedItem);
        }

        /// <inheritdoc/>
        public async Task<EmailBodyResponse> PutItem(Guid id, EmailBodyRequest request)
        {
            var item = await _repository.Get(id);

            if (item == null)
            {
                return null;
            }

            _mapper.Map(request, item);
            await _repository.Update(item);
            return _mapper.Map<EmailBodyResponse>(item);
        }
    }
}
