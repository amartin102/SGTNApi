using System;

namespace Application.Dto
{
    public class TurnoProgramadoDto
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
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

        // Employee basic info
        public string EmployeeName { get; set; }
    }
}
