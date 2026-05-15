using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGS.Class.Models
{
    public class CategoryModel : IEntity    
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
