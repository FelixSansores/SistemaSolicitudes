using SGS.Class.Enums;
using SGS.Class.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        Task<List<RequestModel>> GetFilteredAsync(
             RequestStatus? status,
             RequestPriority? priority,
             int? categoryId,
             string? assignedUserId);
    }
}
