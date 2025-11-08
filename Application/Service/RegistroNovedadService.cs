using Application.Dto;
using Application.Interface;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
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
        private readonly IMaestroPeriodoRepository _periodoRepository;
        private readonly Repository.Interface.IMaestroConceptoRepository _conceptRepository;
        private readonly Repository.Interface.IClientRepository _clientRepository;

        public RegistroNovedadService(IRegistroNovedadRepository repository, IMapper mapper, IMaestroPeriodoRepository periodoRepository, Repository.Interface.IMaestroConceptoRepository conceptRepository, Repository.Interface.IClientRepository clientRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _periodoRepository = periodoRepository;
            _conceptRepository = conceptRepository;
            _clientRepository = clientRepository;
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

        public async Task<RegistroNovedadDto> CreateAsync(CreateRegistroNovedadDto createDto)
        {
            // Basic validation: check that periodo and concepto and empleado exist
            var periodo = await _periodoRepository.GetByIdAsync(createDto.PeriodoNominaId);
            if (periodo == null) throw new ArgumentException("Periodo no existe.");

            // Concept existence (simple check via repository list)
            var conceptos = await _conceptRepository.GetAllWithTipoAsync();
            if (!conceptos.Any(c => c.Id == createDto.ConceptoNovedadId))
                throw new ArgumentException("Concepto de novedad no existe.");

            // Employee existence — use client repository method EmployeeExistsAsync
            if (!await _clientRepository.EmployeeExistsAsync(createDto.EmpleadoId))
                throw new ArgumentException("Empleado no existe o no está activo.");

            var entity = new RegistroNovedad
            {
                Id = Guid.NewGuid(),
                EmpleadoId = createDto.EmpleadoId,
                ConceptoNovedadId = createDto.ConceptoNovedadId,
                PeriodoNominaId = createDto.PeriodoNominaId,
                ValorNovedad = createDto.ValorNovedad,
                FechaNovedad = createDto.FechaNovedad,
                UsuarioCreador = createDto.UsuarioCreador,
                FechaCreacion = createDto.FechaCreacion
            };

            var created = await _repository.AddAsync(entity);

            return new RegistroNovedadDto
            {
                Id = created.Id,
                EmpleadoId = created.EmpleadoId,
                EmpleadoNombre = null,
                EmpleadoIdentificacion = null,
                ConceptoNovedadId = created.ConceptoNovedadId,
                ConceptoNombre = null,
                TipoConceptoId = Guid.Empty,
                TipoConceptoNombre = null,
                PeriodoNominaId = created.PeriodoNominaId,
                PeriodoIdentificador = null,
                ValorNovedad = created.ValorNovedad,
                FechaNovedad = created.FechaNovedad
            };
        }

        public async Task UpdateAsync(Guid id, UpdateRegistroNovedadDto updateDto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException("Registro de novedad no encontrado.");

            // Validate concept exists
            var conceptos = await _conceptRepository.GetAllWithTipoAsync();
            if (!conceptos.Any(c => c.Id == updateDto.ConceptoNovedadId))
                throw new ArgumentException("Concepto de novedad no existe.");

            // Map updates
            entity.ConceptoNovedadId = updateDto.ConceptoNovedadId;
            entity.ValorNovedad = updateDto.ValorNovedad;
            entity.FechaNovedad = updateDto.FechaNovedad;
            entity.ModifiedBy = updateDto.ModificadoPor;
            entity.FechaModificacion = updateDto.FechaModificacion;

            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException("Registro de novedad no encontrado.");

            await _repository.DeleteAsync(entity);
        }
    }
}
