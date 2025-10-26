using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IMasterParameterRepository : IRepository<MasterParameter>
    {
        Task<IEnumerable<MasterParameter>> GetWithDetailsAsync();
        Task<MasterParameter> GetByIdWithDetailsAsync(Guid id);
        Task<bool> IsCodeUniqueAsync(string code, Guid? excludeId = null);
    }
}
