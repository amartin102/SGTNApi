using System;

namespace Domain.Entities
{
    public class MaestroPeriodo
    {
        public Guid Id { get; set; }
        public Guid ValorParametroPeriodicidadId { get; set; }
        public string IdentificadorPeriodo { get; set; }
        public string Descripcion { get; set; }
        public int NumeroPeriodo { get; set; }
        public int? Mes { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime FechaPago { get; set; }
        public DateTime FechaCorte { get; set; }
        public string Estado { get; set; }
        public bool Cerrado { get; set; }
        public string UsuarioCreador { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? FechaModificacion { get; set; }

        // Navigation
        public virtual ParameterValue ValorParametroPeriodicidad { get; set; }
    }
}
