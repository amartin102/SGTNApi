using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ITurnoProgramadoRepository : IRepository<TurnoProgramado>
    {
        Task<IEnumerable<TurnoProgramado>> GetWithDetailsAsync();
        Task<TurnoProgramado> GetByIdWithDetailsAsync(Guid id);
        Task<IEnumerable<TurnoProgramado>> GetByEmployeeIdAsync(Guid employeeId);
        // Additional queries as needed
    }
}
