using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Interface;

namespace Repository.Repositories
{
    public class PermisoRepository : IPermisoRepository
    {
        private readonly SqlDbContext _context;

        public PermisoRepository(SqlDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Permiso>> GetAllAsync()
        {
            return await _context.Permisos
                .OrderBy(p => p.Nombre)
                .ToListAsync();
        }

        public async Task<Permiso?> GetByIdAsync(Guid id)
        {
            return await _context.Permisos
                .FirstOrDefaultAsync(p => p.IdPermiso == id);
        }

        public async Task<Permiso> CreateAsync(Permiso permiso)
        {
            permiso.IdPermiso = Guid.NewGuid();
            _context.Permisos.Add(permiso);
            await _context.SaveChangesAsync();
            return permiso;
        }

        public async Task<Permiso> UpdateAsync(Permiso permiso)
        {
            _context.Permisos.Update(permiso);
            await _context.SaveChangesAsync();
            return permiso;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var permiso = await GetByIdAsync(id);
            if (permiso == null) return false;

            _context.Permisos.Remove(permiso);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Permiso?> GetByCodigoAsync(string codigo)
        {
            return await _context.Permisos
                .FirstOrDefaultAsync(p => p.Codigo == codigo);
        }
    }
}
