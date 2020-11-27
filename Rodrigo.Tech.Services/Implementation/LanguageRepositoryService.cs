using AutoMapper;
using Microsoft.Extensions.Logging;
using Rodrigo.Tech.Models.Response;
using Rodrigo.Tech.Repository.Pattern.Interface;
using Rodrigo.Tech.Repository.Tables;
using Rodrigo.Tech.Services.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rodrigo.Tech.Services.Implementation
{
    public class LanguageRepositoryService : ILanguageRepositoryService
    {
        private readonly IRepositoryPattern<Language> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public LanguageRepositoryService(IRepositoryPattern<Language> repository,
                                        IMapper mapper,
                                        ILogger<LanguageRepositoryService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<LanguageResponse> GetItem(Guid id)
        {
            _logger.LogInformation($"{nameof(LanguageRepositoryService)} " +
                $"- {nameof(GetItem)} - Started, Id: {id}");

            var item = await _repository.Get(id);

            if (item == null)
            {
                _logger.LogError($"{nameof(LanguageRepositoryService)} " +
                $"- {nameof(GetItem)} - Not found, Id: {id}");
                throw new KeyNotFoundException();
            }

            _logger.LogInformation($"{nameof(LanguageRepositoryService)} " +
                $"- {nameof(GetItem)} - Finished, Id: {id}");
            return _mapper.Map<LanguageResponse>(item);
        }

        /// <inheritdoc/>
        public async Task<List<LanguageResponse>> GetItems()
        {
            _logger.LogInformation($"{nameof(LanguageRepositoryService)} " +
                $"- {nameof(GetItems)} - Started");
            var items = await _repository.GetAll();

            if (items.Count == 0)
            {
                _logger.LogInformation($"{nameof(LanguageRepositoryService)} " +
                $"- {nameof(GetItems)} - Not found");
                throw new KeyNotFoundException();
            }

            _logger.LogInformation($"{nameof(LanguageRepositoryService)} " +
                $"- {nameof(GetItems)} - Finished");
            return _mapper.Map<List<LanguageResponse>>(items);
        }
    }
}
