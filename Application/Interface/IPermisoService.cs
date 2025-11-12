using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dto;

namespace Application.Interface
{
    public interface IPermisoService
    {
        Task<IEnumerable<PermisoDto>> GetAllAsync();
        Task<PermisoDto?> GetByIdAsync(Guid id);
        Task<PermisoDto> CreateAsync(CreatePermisoDto createDto);
        Task<PermisoDto> UpdateAsync(Guid id, UpdatePermisoDto updateDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
