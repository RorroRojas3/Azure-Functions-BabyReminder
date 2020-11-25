using AutoMapper;
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

        public LanguageRepositoryService(IRepositoryPattern<Language> repository,
                                        IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<LanguageResponse> GetItem(Guid id)
        {
            var item = await _repository.Get(id);

            if (item == null)
            {
                return null;
            }

            return _mapper.Map<LanguageResponse>(item);
        }

        /// <inheritdoc/>
        public async Task<List<LanguageResponse>> GetItems()
        {
            var items = await _repository.GetAll();

            if (items == null)
            {
                return null;
            }

            return _mapper.Map<List<LanguageResponse>>(items);
        }
    }
}
