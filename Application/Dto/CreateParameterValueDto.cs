using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class CreateParameterValueDto
    {
        public Guid ParameterId { get; set; }
        public Guid ClientId { get; set; }
        public Guid? EmployeeId { get; set; }
        public string TextValue { get; set; }
        public decimal? NumericValue { get; set; }
        public DateTime? DateValue { get; set; }
        public string CreatedBy { get; set; }
    }
}
