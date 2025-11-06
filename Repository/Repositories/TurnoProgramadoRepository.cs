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
    public class TurnoProgramadoRepository : ITurnoProgramadoRepository
    {
        private readonly SqlDbContext _context;

        public TurnoProgramadoRepository(SqlDbContext context)
        {
            _context = context;
        }

        public async Task<TurnoProgramado> AddAsync(TurnoProgramado entity)
        {
            _context.Set<TurnoProgramado>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(TurnoProgramado entity)
        {
            _context.Set<TurnoProgramado>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TurnoProgramado>> GetAllAsync()
        {
            return await _context.Set<TurnoProgramado>().ToListAsync();
        }

        public async Task<IEnumerable<TurnoProgramado>> GetWithDetailsAsync()
        {
            return await _context.Set<TurnoProgramado>()
                .Include(t => t.Employee)
                .ToListAsync();
        }

        public async Task<TurnoProgramado> GetByIdAsync(Guid id)
        {
            return await _context.Set<TurnoProgramado>().FindAsync(id);
        }

        public async Task<TurnoProgramado> GetByIdWithDetailsAsync(Guid id)
        {
            return await _context.Set<TurnoProgramado>()
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TurnoProgramado>> GetByEmployeeIdAsync(Guid employeeId)
        {
            return await _context.Set<TurnoProgramado>()
                .Include(t => t.Employee)
                .Where(t => t.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task UpdateAsync(TurnoProgramado entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
