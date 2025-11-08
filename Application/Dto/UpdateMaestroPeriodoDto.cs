using System;

namespace Application.Dto
{
    public class UpdateMaestroPeriodoDto
    {
        public string IdentificadorPeriodo { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime? FechaPago { get; set; }
        public DateTime? FechaCorte { get; set; }
        public string? ModifiedBy { get; set; }
        public string? Estado { get; set; }
        public bool? Cerrado { get; set; }
    }
}
