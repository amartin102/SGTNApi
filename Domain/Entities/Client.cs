using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Nit { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Cellphone { get; set; }
        public Guid CityId { get; set; }
        public bool Status { get; set; }

        // Navigation properties
        public virtual City City { get; set; }
        public virtual ICollection<ParameterValue> ParameterValues { get; set; }
    }
}
