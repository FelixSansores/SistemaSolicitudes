using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SGS.Class.Interfaces;
using SGS.Class.Models;

namespace SGS.Class.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly SGSDb _context;

        public CommentRepository(SGSDb context)
        {
            _context = context;
        }

        public async Task<List<CommentModel>> GetByRequestIdAsync(int requestId)
        {
            return await _context.Comments
                .Where(c => c.RequestId == requestId)
                .ToListAsync();
        }

        public async Task CreateAsync(CommentModel comment)
        {
            _context.Comments.Add(comment);

            await _context.SaveChangesAsync();
        }
    }
}