using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SGS.Class.Interfaces;
using SGS.Class.Models;

namespace SGS.Class.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _repository;

        public RequestService(IRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RequestModel>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<RequestModel?> GetByIdAsync(int id)
        {
            return await _repository.GetbyIdAsync(id);
        }

        public async Task CreateAsync(RequestModel request)
        {
            request.CreatedAt = DateTime.Now;

            await _repository.AddAsync(request);
        }

        public async Task UpdateAsync(RequestModel request)
        {
            await _repository.updateAsync(request);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
        public async Task<RequestModel?> GetWithCommentsAsync(int id)
        {
            return await _repository.GetWithCommentsAsync(id);
        }
    }
}