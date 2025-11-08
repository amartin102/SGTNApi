using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class RegistroNovedadRepository : IRegistroNovedadRepository
    {
        private readonly SqlDbContext _context;

        public RegistroNovedadRepository(SqlDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RegistroNovedad>> GetByEmpleadoIdAsync(Guid empleadoId)
        {
            return await _context.Set<RegistroNovedad>()
                .Include(r => r.Empleado)
                .Include(r => r.Concepto)
                    .ThenInclude(c => c.TipoConcepto)
                .Include(r => r.Periodo)
                .Where(r => r.EmpleadoId == empleadoId)
                .ToListAsync();
        }

        public async Task<IEnumerable<RegistroNovedad>> GetByPeriodoIdentifierAsync(string identificadorPeriodo)
        {
            var query = from rn in _context.RegistroNovedades
                        join mp in _context.MaestroPeriodos on rn.PeriodoNominaId equals mp.Id
                        where mp.IdentificadorPeriodo == identificadorPeriodo
                        select rn;

            var list = await query
                .Include(r => r.Empleado)
                .Include(r => r.Concepto).ThenInclude(c => c.TipoConcepto)
                .Include(r => r.Periodo)
                .ToListAsync();

            return list;
        }

        public async Task<RegistroNovedad> AddAsync(RegistroNovedad entity)
        {
            _context.RegistroNovedades.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(RegistroNovedad entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<RegistroNovedad> GetByIdAsync(Guid id)
        {
            return await _context.RegistroNovedades
                .Include(r => r.Empleado)
                .Include(r => r.Concepto).ThenInclude(c => c.TipoConcepto)
                .Include(r => r.Periodo)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task DeleteAsync(RegistroNovedad entity)
        {
            _context.RegistroNovedades.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
