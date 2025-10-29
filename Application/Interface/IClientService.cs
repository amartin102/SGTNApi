using Application.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GetActiveClientsAsync();
        Task<IEnumerable<EmployeeWithClientDto>> GetActiveEmployeesWithClientAsync();
    }
}
