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
    public class RolRepository : IRolRepository
    {
        private readonly SqlDbContext _context;

        public RolRepository(SqlDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rol>> GetAllAsync()
        {
            return await _context.Roles
                .OrderBy(r => r.Nombre)
                .ToListAsync();
        }

        public async Task<Rol?> GetByIdAsync(Guid id)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.IdRol == id);
        }

        public async Task<Rol> CreateAsync(Rol rol)
        {
            rol.IdRol = Guid.NewGuid();
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();
            return rol;
        }

        public async Task<Rol> UpdateAsync(Rol rol)
        {
            _context.Roles.Update(rol);
            await _context.SaveChangesAsync();
            return rol;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var rol = await GetByIdAsync(id);
            if (rol == null) return false;

            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Rol>> GetRolesConPermisosAsync()
        {
            return await _context.Roles
                .Include(r => r.RolPermisos)
                .ThenInclude(rp => rp.Permiso)
                .Where(r => r.EstaActivo)
                .OrderBy(r => r.Nombre)
                .ToListAsync();
        }

        public async Task<bool> AsignarPermisosAsync(Guid idRol, List<Guid> idsPermisos)
        {
            // Eliminar permisos existentes
            var permisosExistentes = await _context.RolPermisos
                .Where(rp => rp.IdRol == idRol)
                .ToListAsync();

            _context.RolPermisos.RemoveRange(permisosExistentes);

            // Agregar nuevos permisos
            var nuevosPermisos = idsPermisos.Select(idPermiso => new RolPermiso
            {
                IdRol = idRol,
                IdPermiso = idPermiso
            });

            await _context.RolPermisos.AddRangeAsync(nuevosPermisos);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
