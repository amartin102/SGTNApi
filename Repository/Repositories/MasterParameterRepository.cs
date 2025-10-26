using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Interface;

namespace Repository.Repositories
{
    public class MasterParameterRepository : IMasterParameterRepository
    {
        private readonly SqlDbContext _context;

        public MasterParameterRepository(SqlDbContext context)
        {
            _context = context;
        }

        public async Task<MasterParameter> GetByIdAsync(Guid id)
        {
            return await _context.MasterParameters.FindAsync(id);
        }

        public async Task<MasterParameter> GetByIdWithDetailsAsync(Guid id)
        {
            return await _context.MasterParameters
                .Include(mp => mp.DataType)
                .Include(mp => mp.InconsistencyLevel)
                .FirstOrDefaultAsync(mp => mp.Id == id);
        }

        public async Task<IEnumerable<MasterParameter>> GetAllAsync()
        {
            return await _context.MasterParameters.ToListAsync();
        }

        public async Task<IEnumerable<MasterParameter>> GetWithDetailsAsync()
        {
            return await _context.MasterParameters
                .Include(mp => mp.DataType)
                .Include(mp => mp.InconsistencyLevel)
                .ToListAsync();
        }

        public async Task<MasterParameter> AddAsync(MasterParameter entity)
        {
            _context.MasterParameters.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(MasterParameter entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(MasterParameter entity)
        {
            _context.MasterParameters.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsCodeUniqueAsync(string code, Guid? excludeId = null)
        {
            return await _context.MasterParameters
                .AnyAsync(mp => mp.Code == code && (!excludeId.HasValue || mp.Id != excludeId.Value));
        }
    }
}
