using System;
using System.Collections.Generic;

namespace Application.Dto
{
    public class RolConPermisosDto
    {
        public Guid IdRol { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool EstaActivo { get; set; }
        public List<PermisoDto> Permisos { get; set; } = new List<PermisoDto>();
    }

    public class UsuarioConRolDto
    {
        public Guid IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public bool EstaActivo { get; set; }
        public DateTime? UltimoIngreso { get; set; }
        public RolDto? Rol { get; set; }
    }

    public class AsignarPermisosRolDto
    {
        public Guid IdRol { get; set; }
        public List<Guid> IdsPermisos { get; set; } = new List<Guid>();
    }
}
