using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IRegistroNovedadRepository
    {
        Task<IEnumerable<RegistroNovedad>> GetByEmpleadoIdAsync(Guid empleadoId);
        Task<IEnumerable<RegistroNovedad>> GetByPeriodoIdentifierAsync(string identificadorPeriodo);
        Task<RegistroNovedad> AddAsync(RegistroNovedad entity);
        Task UpdateAsync(RegistroNovedad entity);
        Task<RegistroNovedad> GetByIdAsync(Guid id);
        Task DeleteAsync(RegistroNovedad entity);
    }
}
