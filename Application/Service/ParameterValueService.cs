using Application.Dto;
using Application.Interface;
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
    public class ParameterValueService : IParameterValueService
    {
        private readonly IParameterValueRepository _repository;
        private readonly IMasterParameterRepository _masterParameterRepository;
        private readonly IMapper _mapper;

        public ParameterValueService(
            IParameterValueRepository repository,
            IMasterParameterRepository masterParameterRepository,
            IMapper mapper)
        {
            _repository = repository;
            _masterParameterRepository = masterParameterRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ParameterValueDto>> GetAllAsync()
        {
            var values = await _repository.GetWithDetailsAsync();
            return _mapper.Map<IEnumerable<ParameterValueDto>>(values);
        }

        public async Task<ParameterValueDto> GetByIdAsync(Guid id)
        {
            var value = await _repository.GetByIdWithDetailsAsync(id);
            if (value == null)
                throw new KeyNotFoundException("Valor de parámetro no encontrado.");

            return _mapper.Map<ParameterValueDto>(value);
        }

        public async Task<IEnumerable<ParameterValueDto>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            var values = await _repository.GetByIdsAsync(ids);
            return _mapper.Map<IEnumerable<ParameterValueDto>>(values);
        }

        public async Task<IEnumerable<ParameterValueDto>> GetByParameterIdAsync(Guid parameterId)
        {
            var values = await _repository.GetByParameterIdAsync(parameterId);
            return _mapper.Map<IEnumerable<ParameterValueDto>>(values);
        }

        public async Task<IEnumerable<ParameterValueDto>> GetByClientIdAsync(Guid clientId)
        {
            var values = await _repository.GetByClientIdAsync(clientId);
            return _mapper.Map<IEnumerable<ParameterValueDto>>(values);
        }

        public async Task<IEnumerable<ParameterValueDto>> GetByEmployeeIdAsync(Guid employeeId)
        {
            var values = await _repository.GetByEmployeeIdAsync(employeeId);
            return _mapper.Map<IEnumerable<ParameterValueDto>>(values);
        }

        public async Task<IEnumerable<ParameterValueDto>> GetByClientAndParameterAsync(Guid clientId, Guid parameterId)
        {
            var values = await _repository.GetByClientAndParameterAsync(clientId, parameterId);
            return _mapper.Map<IEnumerable<ParameterValueDto>>(values);
        }

        public async Task<ParameterValueDto> CreateAsync(CreateParameterValueDto createDto)
        {
            // Validate parameter exists
            var parameter = await _masterParameterRepository.GetByIdAsync(createDto.ParameterId);
            if (parameter == null)
                throw new ArgumentException("El parámetro maestro no existe.");

            // Check if value already exists for this combination
            if (await _repository.ExistsAsync(createDto.ParameterId, createDto.ClientId, createDto.EmployeeId))
            {
                throw new ArgumentException("Ya existe un valor para esta combinación de parámetro, cliente y empleado.");
            }

            var parameterValue = _mapper.Map<ParameterValue>(createDto);
            parameterValue.Id = Guid.NewGuid();
            parameterValue.CreationDate = DateTime.UtcNow;

            var created = await _repository.AddAsync(parameterValue);
            return await GetByIdAsync(created.Id); // Return with details
        }

        public async Task UpdateAsync(Guid id, UpdateParameterValueDto updateDto)
        {
            var parameterValue = await _repository.GetByIdAsync(id);
            if (parameterValue == null)
                throw new KeyNotFoundException("Valor de parámetro no encontrado.");

            _mapper.Map(updateDto, parameterValue);
            parameterValue.ModificationDate = DateTime.UtcNow;

            await _repository.UpdateAsync(parameterValue);
        }

        public async Task<bool> UpdateRangeAsync(IEnumerable<UpdateParameterValueWithIdDto> updateDtos)
        {
            try
            {
                var ids = updateDtos.Select(u => u.Id).ToList();
                var entities = (await _repository.GetByIdsAsync(ids)).ToList();

                // Map updates into entities
                foreach (var entity in entities)
                {
                    var update = updateDtos.FirstOrDefault(u => u.Id == entity.Id);
                    if (update == null) continue;

                    // Map fields
                    entity.TextValue = update.TextValue ?? entity.TextValue;
                    entity.NumericValue = update.NumericValue ?? entity.NumericValue;
                    entity.DateValue = update.DateValue ?? entity.DateValue;
                    entity.HourValue = update.HourValue ?? entity.HourValue;
                    entity.ModifiedBy = update.ModifiedBy ?? entity.ModifiedBy;
                    entity.ModificationDate = DateTime.UtcNow;
                }

                await _repository.UpdateRangeAsync(entities);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var parameterValue = await _repository.GetByIdAsync(id);
            if (parameterValue == null)
                throw new KeyNotFoundException("Valor de parámetro no encontrado.");

            await _repository.DeleteAsync(parameterValue);
        }
    }
}
