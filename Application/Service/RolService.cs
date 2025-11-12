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
    public class RolService : IRolService
    {
        private readonly IRolRepository _rolRepository;
        private readonly IMapper _mapper;

        public RolService(IRolRepository rolRepository, IMapper mapper)
        {
            _rolRepository = rolRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RolDto>> GetAllAsync()
        {
            var roles = await _rolRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RolDto>>(roles);
        }

        public async Task<RolDto?> GetByIdAsync(Guid id)
        {
            var rol = await _rolRepository.GetByIdAsync(id);
            return rol != null ? _mapper.Map<RolDto>(rol) : null;
        }

        public async Task<RolDto> CreateAsync(CreateRolDto createDto)
        {
            var rol = _mapper.Map<Rol>(createDto);
            var createdRol = await _rolRepository.CreateAsync(rol);
            return _mapper.Map<RolDto>(createdRol);
        }

        public async Task<RolDto> UpdateAsync(Guid id, UpdateRolDto updateDto)
        {
            var rolExistente = await _rolRepository.GetByIdAsync(id);
            if (rolExistente == null)
                throw new KeyNotFoundException($"Rol con ID {id} no encontrado");

            _mapper.Map(updateDto, rolExistente);
            var updatedRol = await _rolRepository.UpdateAsync(rolExistente);
            return _mapper.Map<RolDto>(updatedRol);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _rolRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<RolConPermisosDto>> GetRolesConPermisosAsync()
        {
            var roles = await _rolRepository.GetRolesConPermisosAsync();
            return _mapper.Map<IEnumerable<RolConPermisosDto>>(roles);
        }

        public async Task<bool> AsignarPermisosAsync(AsignarPermisosRolDto asignarDto)
        {
            return await _rolRepository.AsignarPermisosAsync(asignarDto.IdRol, asignarDto.IdsPermisos);
        }
    }
}
