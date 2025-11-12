using System;

namespace Domain.Entities
{
    public class RolPermiso
    {
        public Guid IdRol { get; set; }
        public Guid IdPermiso { get; set; }

        // Navigation properties
        public virtual Rol Rol { get; set; } = null!;
        public virtual Permiso Permiso { get; set; } = null!;
    }
}
