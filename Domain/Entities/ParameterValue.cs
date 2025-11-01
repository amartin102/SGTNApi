using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ParameterValue
    {
        public Guid Id { get; set; }
        public Guid ParameterId { get; set; }
        public Guid ClientId { get; set; }
        public Guid? EmployeeId { get; set; }
        public string? TextValue { get; set; }
        public decimal? NumericValue { get; set; }
        public DateTime? DateValue { get; set; }

        // Hora/fecha-hora del valor (columna datValorHora en la BD) — ahora TimeSpan (SQL time)
        public TimeSpan? HourValue { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModificationDate { get; set; }

        // Navigation properties
        public virtual MasterParameter MasterParameter { get; set; }
        public virtual Client Client { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
