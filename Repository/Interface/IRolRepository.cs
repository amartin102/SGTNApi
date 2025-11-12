using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Repository.Interface
{
    public interface IRolRepository
    {
        Task<IEnumerable<Rol>> GetAllAsync();
        Task<Rol?> GetByIdAsync(Guid id);
        Task<Rol> CreateAsync(Rol rol);
        Task<Rol> UpdateAsync(Rol rol);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Rol>> GetRolesConPermisosAsync();
        Task<bool> AsignarPermisosAsync(Guid idRol, List<Guid> idsPermisos);
    }
}
