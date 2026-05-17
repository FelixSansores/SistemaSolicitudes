using Microsoft.AspNetCore.Identity;
using SGS.Class.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Class.Models
{
    public class RequestModel:IEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string Location { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ClosedAt { get; set; }

        public RequestStatus Status { get; set; }

        public RequestPriority Priority { get; set; }

        public int? CategoryId { get; set; }

        public CategoryModel? Category { get; set; }

        public ICollection<CommentModel> Comments { get; set; } = new List<CommentModel>();

        public string? AssignedUserId { get; set; }

        public IdentityUser? AssignedUser { get; set; }

    }
}
