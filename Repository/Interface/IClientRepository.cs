using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetActiveClientsAsync();
        Task<IEnumerable<(Employee Employee, Guid? ClientId)>> GetActiveEmployeesWithClientAsync();
    }
}
