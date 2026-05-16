using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SGS.Class.Models;

namespace SGS.Class.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<CommentModel>> GetByRequestIdAsync(int requestId);

        Task CreateAsync(CommentModel comment);
    }
}