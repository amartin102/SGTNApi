using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class UpdateParameterValueDto
    {
        public string TextValue { get; set; }
        public decimal? NumericValue { get; set; }
        public DateTime? DateValue { get; set; }
        public TimeSpan? HourValue { get; set; }
        public string ModifiedBy { get; set; }
    }
}
