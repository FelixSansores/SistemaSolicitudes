using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using SGS.Class.Interfaces;
using SGS.Class.Models;
using SGS.Class;

namespace SGS.Class.Interfaces
{
       public interface IRequestRepository 
        { 
            Task <List<RequestModel>> GetAllAsync();
            Task<RequestModel?> GetbyIdAsync(int id);
            Task AddAsync(RequestModel request);
            Task updateAsync(RequestModel request);
            Task DeleteAsync(int id);
        }
}
