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
    public class TurnoProgramadoService : ITurnoProgramadoService
    {
        private readonly ITurnoProgramadoRepository _repository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public TurnoProgramadoService(ITurnoProgramadoRepository repository, IClientRepository clientRepository, IMapper mapper)
        {
            _repository = repository;
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        private DateTime Combine(DateTime date, TimeSpan time) => date.Date + time;

        private bool Overlaps(DateTime startA, DateTime endA, DateTime startB, DateTime endB)
        {
            // considering inclusive start, exclusive end to avoid touching being considered overlap
            return startA < endB && startB < endA;
        }

        public async Task<TurnoProgramadoDto> CreateAsync(CreateTurnoProgramadoDto createDto)
        {
            // Validate employee exists
            if (!await _clientRepository.EmployeeExistsAsync(createDto.EmployeeId))
                throw new ValidationException("EMPLOYEE_NOT_FOUND", "Empleado no existe o no está activo.");

            // Validate date/time range
            var start = Combine(createDto.FechaInicioProgramada, createDto.HoraInicioProgramada);
            var end = Combine(createDto.FechaFinProgramada, createDto.HoraFinProgramada);
            if (end < start)
                throw new ValidationException("INVALID_PERIOD", "La fecha/hora de fin debe ser mayor o igual a la fecha/hora de inicio.");

            // Check overlaps for same employee
            var existing = await _repository.GetByEmployeeIdAsync(createDto.EmployeeId);
            foreach (var e in existing)
            {
                var eStart = Combine(e.FechaInicioProgramada, e.HoraInicioProgramada);
                var eEnd = Combine(e.FechaFinProgramada, e.HoraFinProgramada);
                if (Overlaps(start, end, eStart, eEnd))
                    throw new ValidationException("OVERLAP", "El turno se solapa con otro turno existente para el empleado.");
            }

            var entity = _mapper.Map<TurnoProgramado>(createDto);
            entity.Id = Guid.NewGuid();
            entity.CreationDate = DateTime.UtcNow;

            var created = await _repository.AddAsync(entity);
            return await GetByIdAsync(created.Id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException("Turno programado no encontrado.");
            await _repository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<TurnoProgramadoDto>> GetAllAsync()
        {
            var items = await _repository.GetWithDetailsAsync();
            return _mapper.Map<IEnumerable<TurnoProgramadoDto>>(items);
        }

        public async Task<TurnoProgramadoDto> GetByIdAsync(Guid id)
        {
            var item = await _repository.GetByIdWithDetailsAsync(id);
            if (item == null) throw new KeyNotFoundException("Turno programado no encontrado.");
            return _mapper.Map<TurnoProgramadoDto>(item);
        }

        public async Task<IEnumerable<TurnoProgramadoDto>> GetByEmployeeIdAsync(Guid employeeId)
        {
            var items = await _repository.GetByEmployeeIdAsync(employeeId);
            return _mapper.Map<IEnumerable<TurnoProgramadoDto>>(items);
        }

        public async Task UpdateAsync(Guid id, UpdateTurnoProgramadoDto updateDto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException("Turno programado no encontrado.");

            // Validate date/time range
            var start = Combine(updateDto.FechaInicioProgramada, updateDto.HoraInicioProgramada);
            var end = Combine(updateDto.FechaFinProgramada, updateDto.HoraFinProgramada);
            if (end < start)
                throw new ValidationException("INVALID_PERIOD", "La fecha/hora de fin debe ser mayor o igual a la fecha/hora de inicio.");

            // Check overlaps for same employee excluding current
            var existing = await _repository.GetByEmployeeIdAsync(entity.EmployeeId);
            foreach (var e in existing.Where(x => x.Id != entity.Id))
            {
                var eStart = Combine(e.FechaInicioProgramada, e.HoraInicioProgramada);
                var eEnd = Combine(e.FechaFinProgramada, e.HoraFinProgramada);
                if (Overlaps(start, end, eStart, eEnd))
                    throw new ValidationException("OVERLAP", "El turno se solapa con otro turno existente para el empleado.");
            }

            _mapper.Map(updateDto, entity);
            entity.ModificationDate = DateTime.UtcNow;
            await _repository.UpdateAsync(entity);
        }
    }
}
