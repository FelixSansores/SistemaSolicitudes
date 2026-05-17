using Azure.Core;
using SGS.Class;
using SGS.Class.Enums;
using SGS.Class.Interfaces;
using SGS.Class.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Class.Interfaces
{
       public interface IRequestRepository 
        { 
            Task <List<RequestModel>> GetAllAsync();
            Task<RequestModel?> GetbyIdAsync(int id);
            Task AddAsync(RequestModel request);
            Task updateAsync(RequestModel request);
            Task DeleteAsync(int id);
            Task<RequestModel?> GetWithCommentsAsync(int id);
        Task<List<RequestModel>> GetFilteredAsync(
            RequestStatus? status,
            RequestPriority? priority,
            int? categoryId,
            string? assignedUserId);
        }
}
