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
    public class MasterParameterService : IMasterParameterService
    {
        private readonly IMasterParameterRepository _repository;
        private readonly IMapper _mapper;

        public MasterParameterService(IMasterParameterRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MasterParameterDto>> GetAllAsync()
        {
            var parameters = await _repository.GetWithDetailsAsync();
            return _mapper.Map<IEnumerable<MasterParameterDto>>(parameters);
        }

        public async Task<MasterParameterDto> GetByIdAsync(Guid id)
        {
            var parameter = await _repository.GetByIdWithDetailsAsync(id);
            return _mapper.Map<MasterParameterDto>(parameter);
        }

        public async Task<MasterParameterDto> CreateAsync(CreateMasterParameterDto createDto)
        {
            if (await _repository.IsCodeUniqueAsync(createDto.Code))
            {
                throw new ArgumentException("El código del parámetro ya existe.");
            }

            var parameter = _mapper.Map<MasterParameter>(createDto);
            parameter.Id = Guid.NewGuid();
            parameter.CreationDate = DateTime.UtcNow;

            var created = await _repository.AddAsync(parameter);
            return _mapper.Map<MasterParameterDto>(created);
        }

        public async Task UpdateAsync(Guid id, UpdateMasterParameterDto updateDto)
        {
            var parameter = await _repository.GetByIdAsync(id);
            if (parameter == null)
                throw new KeyNotFoundException("Parámetro no encontrado.");

            if (await _repository.IsCodeUniqueAsync(updateDto.Code, id))
            {
                throw new ArgumentException("El código del parámetro ya existe.");
            }

            _mapper.Map(updateDto, parameter);
            parameter.ModificationDate = DateTime.UtcNow;

            await _repository.UpdateAsync(parameter);
        }

        public async Task DeleteAsync(Guid id)
        {
            var parameter = await _repository.GetByIdAsync(id);
            if (parameter == null)
                throw new KeyNotFoundException("Parámetro no encontrado.");

            await _repository.DeleteAsync(parameter);
        }
    }
}
