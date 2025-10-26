using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class ParameterValueDto
    {
        public Guid Id { get; set; }
        public Guid ParameterId { get; set; }
        public string ParameterCode { get; set; }
        public string ParameterDescription { get; set; }
        public Guid ClientId { get; set; }
        public string ClientName { get; set; }
        public Guid? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string TextValue { get; set; }
        public decimal? NumericValue { get; set; }
        public DateTime? DateValue { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
