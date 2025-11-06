using System;

namespace Application.Dto
{
    public class UpdateTurnoProgramadoDto
    {
        public DateTime FechaInicioProgramada { get; set; }
        public TimeSpan HoraInicioProgramada { get; set; }
        public DateTime FechaFinProgramada { get; set; }
        public TimeSpan HoraFinProgramada { get; set; }
        public string Estado { get; set; }
        public string Observaciones { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
