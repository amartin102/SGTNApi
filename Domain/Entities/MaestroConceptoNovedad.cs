using System;

namespace Domain.Entities
{
    public class MaestroConceptoNovedad
    {
        public Guid Id { get; set; } // strIdConceptoNovedad
        public string NombreConcepto { get; set; }
        public Guid TipoConceptoId { get; set; }
        public string UsuarioCreador { get; set; }
        public DateTime? FechaCreacion { get; set; }

        // Navigation
        public virtual TipoConcepto TipoConcepto { get; set; }
    }
}
