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
    public class ParameterValueRepository : IParameterValueRepository
    {
        private readonly SqlDbContext _context;

        public ParameterValueRepository(SqlDbContext context)
        {
            _context = context;
        }

        public async Task<ParameterValue> GetByIdAsync(Guid id)
        {
            return await _context.ParameterValues.FindAsync(id);
        }

        public async Task<ParameterValue> GetByIdWithDetailsAsync(Guid id)
        {
            return await _context.ParameterValues
                .Include(pv => pv.MasterParameter)
                    .ThenInclude(mp => mp.DataType)
                .Include(pv => pv.Client)
                .Include(pv => pv.Employee)
                .FirstOrDefaultAsync(pv => pv.Id == id);
        }

        public async Task<IEnumerable<ParameterValue>> GetAllAsync()
        {
            return await _context.ParameterValues.ToListAsync();
        }

        public async Task<IEnumerable<ParameterValue>> GetWithDetailsAsync()
        {
                return await _context.ParameterValues
               .Include(pv => pv.MasterParameter)
                    .ThenInclude(mp => mp.DataType)
               .Include(pv => pv.Client)
               .Include(pv => pv.Employee)
               .ToListAsync();            
        }

        public async Task<IEnumerable<ParameterValue>> GetByParameterIdAsync(Guid parameterId)
        {
            return await _context.ParameterValues
                .Include(pv => pv.MasterParameter)
                    .ThenInclude(mp => mp.DataType)
                .Include(pv => pv.Client)
                .Include(pv => pv.Employee)
                .Where(pv => pv.ParameterId == parameterId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ParameterValue>> GetByClientIdAsync(Guid clientId)
        {
            return await _context.ParameterValues
                .Include(pv => pv.MasterParameter)
                    .ThenInclude(mp => mp.DataType)
                .Include(pv => pv.Client)
                .Include(pv => pv.Employee)
                .Where(pv => pv.ClientId == clientId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ParameterValue>> GetByEmployeeIdAsync(Guid employeeId)
        {
            return await _context.ParameterValues
                .Include(pv => pv.MasterParameter)
                    .ThenInclude(mp => mp.DataType)
                .Include(pv => pv.Client)
                .Include(pv => pv.Employee)
                .Where(pv => pv.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ParameterValue>> GetByClientAndParameterAsync(Guid clientId, Guid parameterId)
        {
            return await _context.ParameterValues
                .Include(pv => pv.MasterParameter)
                    .ThenInclude(mp => mp.DataType)
                .Include(pv => pv.Client)
                .Include(pv => pv.Employee)
                .Where(pv => pv.ClientId == clientId && pv.ParameterId == parameterId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ParameterValue>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _context.ParameterValues
                .Include(pv => pv.MasterParameter)
                    .ThenInclude(mp => mp.DataType)
                .Include(pv => pv.Client)
                .Include(pv => pv.Employee)
                .Where(pv => ids.Contains(pv.Id))
                .ToListAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<ParameterValue> entities)
        {
            _context.ParameterValues.UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<ParameterValue> AddAsync(ParameterValue entity)
        {
            _context.ParameterValues.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(ParameterValue entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ParameterValue entity)
        {
            _context.ParameterValues.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid parameterId, Guid clientId, Guid? employeeId = null)
        {
            return await _context.ParameterValues
                .AnyAsync(pv => pv.ParameterId == parameterId &&
                               pv.ClientId == clientId &&
                               pv.EmployeeId == employeeId);
        }
    }
}
