using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Permiso
    {
        public Guid IdPermiso { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string Codigo { get; set; } = string.Empty;

        // Navigation properties
        public virtual ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();
    }
}
