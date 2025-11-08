using System;

namespace Application.Dto
{
    public class UpdateRegistroNovedadDto
    {
        public Guid ConceptoNovedadId { get; set; } // strIdConceptoNovedad
        public decimal ValorNovedad { get; set; } // decValorNovedad
        public DateTime FechaNovedad { get; set; } // datFechaNovedad
        public DateTime FechaModificacion { get; set; } // datFechaModificacion
        public string ModificadoPor { get; set; } // strModificadoPor
    }
}
