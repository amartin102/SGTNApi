using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dto;

namespace Application.Interface
{
    public interface IRolService
    {
        Task<IEnumerable<RolDto>> GetAllAsync();
        Task<RolDto?> GetByIdAsync(Guid id);
        Task<RolDto> CreateAsync(CreateRolDto createDto);
        Task<RolDto> UpdateAsync(Guid id, UpdateRolDto updateDto);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<RolConPermisosDto>> GetRolesConPermisosAsync();
        Task<bool> AsignarPermisosAsync(AsignarPermisosRolDto asignarDto);
    }
}
