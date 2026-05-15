using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SGS.Class.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Class
{
    public class SGSDb : IdentityDbContext
    {
        public SGSDb(DbContextOptions<SGSDb> options)
            : base(options)
        {
        }

        public DbSet<RequestModel> Requests { get; set; }

        public DbSet<CommentModel> Comments { get; set; }

        public DbSet<CategoryModel> Categories { get; set; }

        public DbSet<PriorityModel> Priorities { get; set; }

        public DbSet<StatusModel> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CommentModel>()
                .HasOne(c => c.Request)
                .WithMany()
                .HasForeignKey(c => c.RequestModelId);
        }
    }
}
