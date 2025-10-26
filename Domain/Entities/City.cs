using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class City
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid DepartmentId { get; set; }

        // Navigation properties
        public virtual Department Department { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
    }
}
