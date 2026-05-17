using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SGS.Class;
using SGS.Class.Interfaces;
using SGS.Class.Models;
namespace SGS.Class.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly SGSDb _context;
        public RequestRepository(SGSDb context)
        {
            _context = context;
        }
        public async Task<List<RequestModel>> GetAllAsync()
        {
            return await _context.Requests
                .Include(r => r.Category)
                .Include(r => r.AssignedUser)
                .ToListAsync();
        }
        public async Task<RequestModel?> GetbyIdAsync(int id)
        {
            return await _context.Requests.FindAsync(id);
        }
        public async Task AddAsync(RequestModel request)
        {
            await _context.Requests.AddAsync(request);
            await _context.SaveChangesAsync();
        }
        public async Task updateAsync(RequestModel request)
        {
            _context.Requests.Update(request);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request != null)
            {
                _context.Requests.Remove(request);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<RequestModel?> GetWithCommentsAsync(int id)
        {
            return await _context.Requests
                .Include(r => r.Category)
                .Include(r => r.AssignedUser)
                .Include(r => r.Comments)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
