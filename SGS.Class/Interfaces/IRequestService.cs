using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGS.Class.Models;

namespace SGS.Class.Interfaces
{
    public interface IRequestService
    {
        Task<List<RequestModel>> GetAllAsync();

        Task<RequestModel?> GetByIdAsync(int id);

        Task CreateAsync(RequestModel request);

        Task UpdateAsync(RequestModel request);

        Task DeleteAsync(int id);
        Task<RequestModel?> GetWithCommentsAsync(int id);
    }
}
