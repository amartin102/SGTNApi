using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Country
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        // Navigation properties
        public virtual ICollection<Department> Departments { get; set; }
    }
}
