using System.Diagnostics.CodeAnalysis;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Interface;


namespace Repository.Repositories
{
    [ExcludeFromCodeCoverage]
    public class ParameterMasterRepository : Repository<ParameterMasterEntity, SqlDbContext>, IParameterMasterRepository
    {
        public ParameterMasterRepository(SqlDbContext context): base(context) { }

        public async Task<ParameterMasterEntity> CreateParameter(ParameterMasterEntity entity)
        {            
            var result = await AddAsync(entity);
            return result;
        }

        public async Task<bool> UpdateParameter(ParameterMasterEntity entity)
        {
            var result = await UpdateAsync(entity);
            return result;
        }

        public async Task<ParameterMasterEntity> GetParameterById(Guid id)
        {
            var result = await GetByIdAsync(id);                      
            return result;
        }

    }
}
