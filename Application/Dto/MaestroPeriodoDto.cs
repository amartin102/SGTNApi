using System;

namespace Application.Dto
{
    public class MaestroPeriodoDto
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

        // New field
        public string Periodicidad { get; set; }
    }
}
