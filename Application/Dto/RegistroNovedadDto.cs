using System;

namespace Application.Dto
{
    public class RegistroNovedadDto
    {
        public Guid Id { get; set; }
        public Guid EmpleadoId { get; set; }
        public string EmpleadoNombre { get; set; }
        public string EmpleadoIdentificacion { get; set; }
        public Guid ConceptoNovedadId { get; set; }
        public string ConceptoNombre { get; set; }
        public Guid TipoConceptoId { get; set; }
        public string TipoConceptoNombre { get; set; }
        public Guid PeriodoNominaId { get; set; }
        public string PeriodoIdentificador { get; set; }
        public decimal ValorNovedad { get; set; }
        public DateTime FechaNovedad { get; set; }
    }
}
