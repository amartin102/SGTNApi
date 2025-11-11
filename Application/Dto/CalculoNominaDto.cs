using System;

namespace Application.Dto
{
    public class CalculoNominaDto
    {
        public int IntIdNomina { get; set; }
        public string StrIdPeriodo_IdPeriodo { get; set; }
        public string IdentificadorPeriodo { get; set; }
        public DateTime FechaInicioPeriodo { get; set; }
        public DateTime FechaFinPeriodo { get; set; }
        public string NombreCliente { get; set; }
        public string StrNit { get; set; }
        public string NitCliente { get; set; }
        public string StrNombre { get; set; }
        public string NombreEmpleado { get; set; }
        public string StrApellido { get; set; }
        public string ApellidoEmpleado { get; set; }
        public string NombreCompletoEmpleado { get; set; }
        public string StrIdentificacion { get; set; }
        public string IdentificacionEmpleado { get; set; }
        public string StrIdentificador { get; set; }
        public string NombreEmpleadoNomina { get; set; }
        public decimal TotalDevengadoPeriodo { get; set; }
        public decimal TotalAdicionesPeriodo { get; set; }
        public decimal TotalDeduccionesPeriodo { get; set; }
        public decimal TotalNetoPeriodo { get; set; }
        public int DiasTrabajados { get; set; }
        public decimal TotalHorasTrabajadas { get; set; }
        public decimal TotalHorasExtras { get; set; }
        public decimal TotalCantidadAdiciones { get; set; }
        public decimal TotalCantidadDeducciones { get; set; }
        public decimal SaludTrabajador { get; set; }
        public decimal PensionTrabajador { get; set; }
        public decimal TotalDeduccionesTrabajador { get; set; }
        public decimal SalarioNetoFinal { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string StrDescripcion { get; set; }
        public string DescripcionPeriodo { get; set; }
    }
}
