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
    }
}
