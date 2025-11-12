using System;

namespace Application.Dto
{
    public class UsuarioDto
    {
        public Guid IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public Guid IdRol { get; set; }
        public string? NombreRol { get; set; }
        public bool EstaActivo { get; set; }
        public DateTime? UltimoIngreso { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }

    public class CreateUsuarioDto
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public Guid IdRol { get; set; }
        public bool EstaActivo { get; set; } = true;
        public string? UsuarioCreacion { get; set; }
    }

    public class UpdateUsuarioDto
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public string? Contrasena { get; set; }
        public Guid IdRol { get; set; }
        public bool EstaActivo { get; set; }
        public string? UsuarioModificacion { get; set; }
    }
}
