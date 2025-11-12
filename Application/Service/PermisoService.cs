using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dto;
using Application.Interface;
using AutoMapper;
using Domain.Entities;
using Repository.Interface;

namespace Application.Service
{
    public class PermisoService : IPermisoService
    {
        private readonly IPermisoRepository _permisoRepository;
        private readonly IMapper _mapper;

        public PermisoService(IPermisoRepository permisoRepository, IMapper mapper)
        {
            _permisoRepository = permisoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PermisoDto>> GetAllAsync()
        {
            var permisos = await _permisoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PermisoDto>>(permisos);
        }

        public async Task<PermisoDto?> GetByIdAsync(Guid id)
        {
            var permiso = await _permisoRepository.GetByIdAsync(id);
            return permiso != null ? _mapper.Map<PermisoDto>(permiso) : null;
        }

        public async Task<PermisoDto> CreateAsync(CreatePermisoDto createDto)
        {
            var permiso = _mapper.Map<Permiso>(createDto);
            var createdPermiso = await _permisoRepository.CreateAsync(permiso);
            return _mapper.Map<PermisoDto>(createdPermiso);
        }

        public async Task<PermisoDto> UpdateAsync(Guid id, UpdatePermisoDto updateDto)
        {
            var permisoExistente = await _permisoRepository.GetByIdAsync(id);
            if (permisoExistente == null)
                throw new KeyNotFoundException($"Permiso con ID {id} no encontrado");

            _mapper.Map(updateDto, permisoExistente);
            var updatedPermiso = await _permisoRepository.UpdateAsync(permisoExistente);
            return _mapper.Map<PermisoDto>(updatedPermiso);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _permisoRepository.DeleteAsync(id);
        }
    }
}
