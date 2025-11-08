using System;

namespace Domain.Entities
{
    public class RegistroNovedad
    {
        public Guid Id { get; set; } // strIdNovedad
        public Guid EmpleadoId { get; set; }
        public Guid ConceptoNovedadId { get; set; }
        public Guid PeriodoNominaId { get; set; }
        public decimal ValorNovedad { get; set; }
        public DateTime FechaNovedad { get; set; }
        public string UsuarioCreador { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? FechaModificacion { get; set; }

        // Navigation
        public virtual Employee Empleado { get; set; }
        public virtual MaestroConceptoNovedad Concepto { get; set; }
        public virtual MaestroPeriodo Periodo { get; set; }
    }
}
