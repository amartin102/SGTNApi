using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }
        public int IdentificationTypeId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Cellphone { get; set; }
        public bool Status { get; set; }

        // Navigation properties
        public virtual IdentificationType IdentificationType { get; set; }
        public virtual ICollection<ParameterValue> ParameterValues { get; set; }
    }
}
