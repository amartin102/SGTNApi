using Application.Dto;
using Application.Interface;
using AutoMapper;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class RegistroNovedadService : IRegistroNovedadService
    {
        private readonly IRegistroNovedadRepository _repository;
        private readonly IMapper _mapper;

        public RegistroNovedadService(IRegistroNovedadRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RegistroNovedadDto>> GetByEmpleadoIdAsync(Guid empleadoId)
        {
            var items = await _repository.GetByEmpleadoIdAsync(empleadoId);
            return items.Select(r => new RegistroNovedadDto
            {
                Id = r.Id,
                EmpleadoId = r.EmpleadoId,
                EmpleadoNombre = r.Empleado != null ? $"{r.Empleado.FirstName} {r.Empleado.LastName}" : null,
                EmpleadoIdentificacion = r.Empleado?.Identification,
                ConceptoNovedadId = r.ConceptoNovedadId,
                ConceptoNombre = r.Concepto != null ? r.Concepto.NombreConcepto : null,
                TipoConceptoId = r.Concepto?.TipoConceptoId ?? Guid.Empty,
                TipoConceptoNombre = r.Concepto?.TipoConcepto?.NombreTipo,
                PeriodoNominaId = r.PeriodoNominaId,
                PeriodoIdentificador = r.Periodo?.IdentificadorPeriodo,
                ValorNovedad = r.ValorNovedad,
                FechaNovedad = r.FechaNovedad
            }).ToList();
        }

        public async Task<IEnumerable<RegistroNovedadDto>> GetByPeriodoIdentifierAsync(string identificadorPeriodo)
        {
            var items = await _repository.GetByPeriodoIdentifierAsync(identificadorPeriodo);
            return items.Select(r => new RegistroNovedadDto
            {
                Id = r.Id,
                EmpleadoId = r.EmpleadoId,
                EmpleadoNombre = r.Empleado != null ? $"{r.Empleado.FirstName} {r.Empleado.LastName}" : null,
                EmpleadoIdentificacion = r.Empleado?.Identification,
                ConceptoNovedadId = r.ConceptoNovedadId,
                ConceptoNombre = r.Concepto != null ? r.Concepto.NombreConcepto : null,
                TipoConceptoId = r.Concepto?.TipoConceptoId ?? Guid.Empty,
                TipoConceptoNombre = r.Concepto?.TipoConcepto?.NombreTipo,
                PeriodoNominaId = r.PeriodoNominaId,
                PeriodoIdentificador = r.Periodo?.IdentificadorPeriodo,
                ValorNovedad = r.ValorNovedad,
                FechaNovedad = r.FechaNovedad
            }).ToList();
        }
    }
}
