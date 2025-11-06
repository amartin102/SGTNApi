using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TurnoProgramado
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }

        // Nuevo esquema: fecha y hora de inicio/fin separados
        public DateTime FechaInicioProgramada { get; set; }
        public TimeSpan HoraInicioProgramada { get; set; }
        public DateTime FechaFinProgramada { get; set; }
        public TimeSpan HoraFinProgramada { get; set; }

        public string Estado { get; set; }
        public string Observaciones { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModificationDate { get; set; }

        // Navigation
        public virtual Employee Employee { get; set; }
    }
}
