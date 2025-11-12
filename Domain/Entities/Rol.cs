using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Rol
    {
        public Guid IdRol { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool EstaActivo { get; set; } = true;
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        // Navigation properties
        public virtual ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();
        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
