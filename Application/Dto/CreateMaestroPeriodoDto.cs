using System;

namespace Application.Dto
{
    public class CreateMaestroPeriodoDto
    {
        public Guid ValorParametroPeriodicidadId { get; set; }
        public Guid? ClientId { get; set; }
        public string IdentificadorPeriodo { get; set; }
        public string Descripcion { get; set; }
        public int NumeroPeriodo { get; set; }
        public int? Mes { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime FechaPago { get; set; }
        public DateTime FechaCorte { get; set; }
        public string UsuarioCreador { get; set; }
    }
}
