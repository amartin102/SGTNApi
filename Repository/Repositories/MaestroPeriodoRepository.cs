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
    public class MaestroPeriodoRepository : IMaestroPeriodoRepository
    {
        private readonly SqlDbContext _context;

        public MaestroPeriodoRepository(SqlDbContext context)
        {
            _context = context;
        }

        public async Task<MaestroPeriodo> AddAsync(MaestroPeriodo entity)
        {
            _context.Set<MaestroPeriodo>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(MaestroPeriodo entity)
        {
            _context.Set<MaestroPeriodo>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<MaestroPeriodo> GetByIdAsync(Guid id)
        {
            return await _context.Set<MaestroPeriodo>().FindAsync(id);
        }

        public async Task<IEnumerable<MaestroPeriodo>> GetByValorParametroPeriodicidadIdAsync(Guid valorParametroId)
        {
            return await _context.Set<MaestroPeriodo>()
                .Where(p => p.ValorParametroPeriodicidadId == valorParametroId)
                .OrderByDescending(p => p.FechaInicio)
                .ToListAsync();
        }

        public async Task<IEnumerable<MaestroPeriodo>> GetByClientIdViaParameterValueAsync(Guid clientId)
        {
            // Join MaestroPeriodo with ParameterValues where MaestroPeriodo.ValorParametroPeriodicidadId == ParameterValue.Id
            var rows = await (from mp in _context.MaestroPeriodos
                        join pv in _context.ParameterValues on mp.ValorParametroPeriodicidadId equals pv.Id
                        where pv.ClientId == clientId
                        select new { mp, pv }).ToListAsync();

            // Attach the ParameterValue navigation property so AutoMapper can map TextValue
            var result = rows
                .GroupBy(r => r.mp.Id)
                .Select(g =>
                {
                    var mp = g.First().mp;
                    // assign the first matching pv (there should be at least one)
                    mp.ValorParametroPeriodicidad = g.First().pv;
                    return mp;
                })
                .OrderByDescending(p => p.FechaInicio)
                .ToList();

            return result;
        }

        public async Task UpdateAsync(MaestroPeriodo entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<MaestroPeriodo> entities)
        {
            _context.Set<MaestroPeriodo>().UpdateRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}
