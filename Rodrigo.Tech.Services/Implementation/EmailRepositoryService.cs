using AutoMapper;
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

        public EmailRepositoryService(IRepositoryPattern<Email> repository,
                                        IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteItem(Guid id)
        {
            return await _repository.Delete(id);
        }

        /// <inheritdoc/>
        public async Task<EmailResponse> GetItem(Guid id)
        {
            var item = await _repository.Get(id);

            if (item == null)
            {
                return null;
            }

            return _mapper.Map<EmailResponse>(item);
        }

        /// <inheritdoc/>
        public async Task<List<EmailResponse>> GetItems()
        {
            var items = await _repository.GetAll();

            if (items == null)
            {
                return null;
            }

            return _mapper.Map<List<EmailResponse>>(items);
        }

        /// <inheritdoc/>
        public async Task<EmailResponse> PostItem(EmailRequest item)
        {
            var newItem = _mapper.Map<Email>(item);

            var addedItem = await _repository.Add(newItem);

            return _mapper.Map<EmailResponse>(addedItem);
        }

        /// <inheritdoc/>
        public async Task<EmailResponse> PutItem(Guid id, EmailRequest request)
        {
            var item = await _repository.Get(id);

            if (item == null)
            {
                return null;
            }

            _mapper.Map(request, item);
            await _repository.Update(item);
            return _mapper.Map<EmailResponse>(item);
        }
    }
}
