using System;

namespace Application.Dto
{
    public class RolDto
    {
        public Guid IdRol { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool EstaActivo { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }

    public class CreateRolDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool EstaActivo { get; set; } = true;
        public string? UsuarioCreacion { get; set; }
    }

    public class UpdateRolDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool EstaActivo { get; set; }
        public string? UsuarioModificacion { get; set; }
    }
}
