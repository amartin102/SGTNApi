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
    public class MaestroPeriodoService : IMaestroPeriodoService
    {
        private readonly IMaestroPeriodoRepository _repository;
        private readonly IParameterValueRepository _parameterValueRepository;
        private readonly IMapper _mapper;

        public MaestroPeriodoService(IMaestroPeriodoRepository repository, IParameterValueRepository parameterValueRepository, IMapper mapper)
        {
            _repository = repository;
            _parameterValueRepository = parameterValueRepository;
            _mapper = mapper;
        }

        public async Task<MaestroPeriodoDto> CreateAsync(CreateMaestroPeriodoDto createDto)
        {
            var entity = _mapper.Map<MaestroPeriodo>(createDto);
            entity.Id = Guid.NewGuid();
            entity.FechaCreacion = DateTime.UtcNow;

            var created = await _repository.AddAsync(entity);
            return _mapper.Map<MaestroPeriodoDto>(created);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException("Periodo no encontrado.");
            await _repository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<MaestroPeriodoDto>> GetByValorParametroPeriodicidadIdAsync(Guid valorParametroId)
        {
            var items = await _repository.GetByValorParametroPeriodicidadIdAsync(valorParametroId);
            return _mapper.Map<IEnumerable<MaestroPeriodoDto>>(items);
        }

        public async Task<MaestroPeriodoDto> GetByIdAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null) throw new KeyNotFoundException("Periodo no encontrado.");
            return _mapper.Map<MaestroPeriodoDto>(item);
        }

        public async Task UpdateAsync(Guid id, UpdateMaestroPeriodoDto updateDto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException("Periodo no encontrado.");

            _mapper.Map(updateDto, entity);
            entity.FechaModificacion = DateTime.UtcNow;
            await _repository.UpdateAsync(entity);
        }

        public async Task<MaestroPeriodoDto> ApplyPeriodAsync(Guid id, bool force = false)
        {
            var period = await _repository.GetByIdAsync(id);
            if (period == null) throw new KeyNotFoundException("Periodo no encontrado.");
            if (period.Cerrado && !force)
                throw new ValidationException("PERIOD_CLOSED", "El periodo está cerrado y no se puede aplicar.");

            // Lógica simple: devolver parámetros que aplican a esa ventana de fechas
            var values = await _parameterValueRepository.GetWithDetailsAsync();
            var matched = values.Where(v => v.DateValue.HasValue && v.DateValue.Value.Date >= period.FechaInicio.Date && v.DateValue.Value.Date <= period.FechaFin.Date);

            return _mapper.Map<MaestroPeriodoDto>(period);
        }

        public async Task<IEnumerable<MaestroPeriodoDto>> GetByClientIdAsync(Guid clientId)
        {
            var items = await _repository.GetByClientIdViaParameterValueAsync(clientId);
            return _mapper.Map<IEnumerable<MaestroPeriodoDto>>(items);
        }

        public async Task UpdateRangeForClientAsync(Guid clientId, IEnumerable<MaestroPeriodoDto> updateDtos)
        {
            var existing = (await _repository.GetByClientIdViaParameterValueAsync(clientId)).ToList();
            if (!existing.Any()) return;

            var toUpdate = new List<MaestroPeriodo>();

            foreach (var dto in updateDtos)
            {
                var match = existing.FirstOrDefault(e => e.Id == dto.Id);
                if (match != null)
                {
                    _mapper.Map(dto, match);
                    match.FechaModificacion = DateTime.UtcNow;
                    toUpdate.Add(match);
                }
            }

            if (toUpdate.Any())
            {
                await _repository.UpdateRangeAsync(toUpdate);
            }
        }
    }
}
