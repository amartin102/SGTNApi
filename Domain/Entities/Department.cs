using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid CountryId { get; set; }

        // Navigation properties
        public virtual Country Country { get; set; }
        public virtual ICollection<City> Cities { get; set; }
    }

}
