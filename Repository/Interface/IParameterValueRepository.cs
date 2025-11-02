using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IParameterValueRepository : IRepository<ParameterValue>
    {
        Task<IEnumerable<ParameterValue>> GetWithDetailsAsync();
        Task<ParameterValue> GetByIdWithDetailsAsync(Guid id);
        Task<IEnumerable<ParameterValue>> GetByParameterIdAsync(Guid parameterId);
        Task<IEnumerable<ParameterValue>> GetByClientIdAsync(Guid clientId);
        Task<IEnumerable<ParameterValue>> GetByEmployeeIdAsync(Guid employeeId);
        Task<IEnumerable<ParameterValue>> GetByClientAndParameterAsync(Guid clientId, Guid parameterId);
        Task<bool> ExistsAsync(Guid parameterId, Guid clientId, Guid? employeeId = null);

        // Batch operations
        Task<IEnumerable<ParameterValue>> GetByIdsAsync(IEnumerable<Guid> ids);
        Task UpdateRangeAsync(IEnumerable<ParameterValue> entities);
    }
}
