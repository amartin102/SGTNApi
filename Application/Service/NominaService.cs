using Application.Dto;
using Application.Interface;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service
{
    public class NominaService : INominaService
    {
        private readonly INominaRepository _repository;

        public NominaService(INominaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CalculoNominaDto>> CalcularNominaAsync(string strIdPeriodo, string strIdCliente)
        {
            var results = await _repository.CalcularNominaCompletaAsync(strIdPeriodo, strIdCliente);

            return results.Select(row => new CalculoNominaDto
            {
                IntIdNomina = row.ContainsKey("intIdNomina") ? Convert.ToInt32(row["intIdNomina"]) : 0,
                StrIdPeriodo_IdPeriodo = row.ContainsKey("strIdPeriodo_IdPeriodo") ? row["strIdPeriodo_IdPeriodo"]?.ToString() : null,
                IdentificadorPeriodo = row.ContainsKey("IdentificadorPeriodo") ? row["IdentificadorPeriodo"]?.ToString() : null,
                FechaInicioPeriodo = row.ContainsKey("FechaInicioPeriodo") ? Convert.ToDateTime(row["FechaInicioPeriodo"]) : DateTime.MinValue,
                FechaFinPeriodo = row.ContainsKey("FechaFinPeriodo") ? Convert.ToDateTime(row["FechaFinPeriodo"]) : DateTime.MinValue,
                NombreCliente = row.ContainsKey("NombreCliente") ? row["NombreCliente"]?.ToString() : "",
                StrNit = row.ContainsKey("strNit") ? row["strNit"]?.ToString() : null,
                NitCliente = row.ContainsKey("NitCliente") ? row["NitCliente"]?.ToString() : "" ,
                StrNombre = row.ContainsKey("strNombre") ? row["strNombre"]?.ToString() : null,
                NombreEmpleado = row.ContainsKey("NombreEmpleado") ? row["NombreEmpleado"]?.ToString() : null,
                StrApellido = row.ContainsKey("strApellido") ? row["strApellido"]?.ToString() : null,
                ApellidoEmpleado = row.ContainsKey("ApellidoEmpleado") ? row["ApellidoEmpleado"]?.ToString() : null,
                NombreCompletoEmpleado = row.ContainsKey("NombreCompletoEmpleado") ? row["NombreCompletoEmpleado"]?.ToString() : null,
                StrIdentificacion = row.ContainsKey("strIdentificacion") ? row["strIdentificacion"]?.ToString() : null,
                IdentificacionEmpleado = row.ContainsKey("IdentificacionEmpleado") ? row["IdentificacionEmpleado"]?.ToString() : null,
                StrIdentificador = row.ContainsKey("strIdentificador") ? row["strIdentificador"]?.ToString() : null,
                NombreEmpleadoNomina = row.ContainsKey("NombreEmpleadoNomina") ? row["NombreEmpleadoNomina"]?.ToString() : null,
                TotalDevengadoPeriodo = row.ContainsKey("TotalDevengadoPeriodo") ? Convert.ToDecimal(row["TotalDevengadoPeriodo"]) : 0,
                TotalAdicionesPeriodo = row.ContainsKey("TotalAdicionesPeriodo") ? Convert.ToDecimal(row["TotalAdicionesPeriodo"]) : 0,
                TotalDeduccionesPeriodo = row.ContainsKey("TotalDeduccionesPeriodo") ? Convert.ToDecimal(row["TotalDeduccionesPeriodo"]) : 0,
                TotalNetoPeriodo = row.ContainsKey("TotalNetoPeriodo") ? Convert.ToDecimal(row["TotalNetoPeriodo"]) : 0,
                DiasTrabajados = row.ContainsKey("DiasTrabajados") ? Convert.ToInt32(row["DiasTrabajados"]) : 0,
                TotalHorasTrabajadas = row.ContainsKey("TotalHorasTrabajadas") ? Convert.ToDecimal(row["TotalHorasTrabajadas"]) : 0,
                TotalHorasExtras = row.ContainsKey("TotalHorasExtras") ? Convert.ToDecimal(row["TotalHorasExtras"]) : 0,
                TotalCantidadAdiciones = row.ContainsKey("TotalCantidadAdiciones") ? Convert.ToDecimal(row["TotalCantidadAdiciones"]) : 0,
                TotalCantidadDeducciones = row.ContainsKey("TotalCantidadDeducciones") ? Convert.ToDecimal(row["TotalCantidadDeducciones"]) : 0,
                SaludTrabajador = row.ContainsKey("SaludTrabajador") ? Convert.ToDecimal(row["SaludTrabajador"]) : 0,
                PensionTrabajador = row.ContainsKey("PensionTrabajador") ? Convert.ToDecimal(row["PensionTrabajador"]) : 0,
                TotalDeduccionesTrabajador = row.ContainsKey("TotalDeduccionesTrabajador") ? Convert.ToDecimal(row["TotalDeduccionesTrabajador"]) : 0,
                SalarioNetoFinal = row.ContainsKey("SalarioNetoFinal") ? Convert.ToDecimal(row["SalarioNetoFinal"]) : 0,
                FechaCreacion = row.ContainsKey("FechaCreacion") ? Convert.ToDateTime(row["FechaCreacion"]) : DateTime.MinValue,
                StrDescripcion = row.ContainsKey("strDescripcion") ? row["strDescripcion"]?.ToString() : null,
                DescripcionPeriodo = row.ContainsKey("DescripcionPeriodo") ? row["DescripcionPeriodo"]?.ToString() : null
            }).ToList();
        }
    }
}
