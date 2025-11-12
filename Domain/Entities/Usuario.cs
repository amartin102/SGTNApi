using System;

namespace Domain.Entities
{
    public class Usuario
    {
        public Guid IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public Guid IdRol { get; set; }
        public bool EstaActivo { get; set; } = true;
        public DateTime? UltimoIngreso { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        // Navigation properties
        public virtual Rol Rol { get; set; } = null!;
    }
}
