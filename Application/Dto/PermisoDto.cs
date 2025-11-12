using System;

namespace Application.Dto
{
    public class PermisoDto
    {
        public Guid IdPermiso { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string Codigo { get; set; } = string.Empty;
    }

    public class CreatePermisoDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string Codigo { get; set; } = string.Empty;
    }

    public class UpdatePermisoDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string Codigo { get; set; } = string.Empty;
    }
}
