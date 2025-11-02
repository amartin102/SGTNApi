using System;

namespace Application.Dto
{
    public class UpdateParameterValueWithIdDto
    {
        public Guid Id { get; set; }
        public string TextValue { get; set; }
        public decimal? NumericValue { get; set; }
        public DateTime? DateValue { get; set; }
        public TimeSpan? HourValue { get; set; }
        public string ModifiedBy { get; set; }
    }
}
