using System;

namespace Application.Dto
{
    public class CreateRegistroNovedadDto
    {
        public Guid EmpleadoId { get; set; } // strIdEmpleado
        public Guid ConceptoNovedadId { get; set; } // strIdConceptoNovedad
        public Guid PeriodoNominaId { get; set; } // strIdPeriodoNomina
        public decimal ValorNovedad { get; set; } // decValorNovedad
        public DateTime FechaNovedad { get; set; } // datFechaNovedad
        public string UsuarioCreador { get; set; } // strUsuarioCreador
        public DateTime FechaCreacion { get; set; } // datFechaCreacion
    }
}
