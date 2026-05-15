using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Class.Models
{
    public class CommentModel : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public int RequestModelId { get; set; }

        public RequestModel Request { get; set; }
    }
}
