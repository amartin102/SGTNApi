using System;

namespace Application.Dto
{
    public class CreateTurnoProgramadoDto
    {
        public Guid EmployeeId { get; set; }
        public DateTime FechaInicioProgramada { get; set; }
        public TimeSpan HoraInicioProgramada { get; set; }
        public DateTime FechaFinProgramada { get; set; }
        public TimeSpan HoraFinProgramada { get; set; }
        public string Estado { get; set; }
        public string Observaciones { get; set; }
        public string CreatedBy { get; set; }
    }
}
