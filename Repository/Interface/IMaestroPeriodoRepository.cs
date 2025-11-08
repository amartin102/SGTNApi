using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IMaestroPeriodoRepository
    {
        Task<IEnumerable<MaestroPeriodo>> GetByValorParametroPeriodicidadIdAsync(Guid valorParametroId);
        Task<MaestroPeriodo> GetByIdAsync(Guid id);
        Task<MaestroPeriodo> AddAsync(MaestroPeriodo entity);
        Task UpdateAsync(MaestroPeriodo entity);
        Task DeleteAsync(MaestroPeriodo entity);

        // New methods
        Task<IEnumerable<MaestroPeriodo>> GetByClientIdViaParameterValueAsync(Guid clientId);
        Task UpdateRangeAsync(IEnumerable<MaestroPeriodo> entities);
    }
}
