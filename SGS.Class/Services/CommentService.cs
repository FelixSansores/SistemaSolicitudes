using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGS.Class.Interfaces;
using SGS.Class.Models;

namespace SGS.Class.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;

        public CommentService(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CommentModel>> GetByRequestIdAsync(int requestId)
        {
            return await _repository.GetByRequestIdAsync(requestId);
        }

        public async Task CreateAsync(CommentModel comment)
        {
            await _repository.CreateAsync(comment);
        }
    }
}