using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Repository.Interface
{
    public interface IPermisoRepository
    {
        Task<IEnumerable<Permiso>> GetAllAsync();
        Task<Permiso?> GetByIdAsync(Guid id);
        Task<Permiso> CreateAsync(Permiso permiso);
        Task<Permiso> UpdateAsync(Permiso permiso);
        Task<bool> DeleteAsync(Guid id);
        Task<Permiso?> GetByCodigoAsync(string codigo);
    }
}
