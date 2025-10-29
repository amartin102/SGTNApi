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
    public class ClientsRepository : IClientRepository
    {
        private readonly SqlDbContext _context;

        public ClientsRepository(SqlDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetActiveClientsAsync()
        {
            return await _context.Clients
                .Where(c => c.Status)
                .ToListAsync();
        }

        public async Task<IEnumerable<(Employee Employee, Guid? ClientId)>> GetActiveEmployeesWithClientAsync()
        {
            var employees = await _context.Employees
                .Where(e => e.Status)
                .ToListAsync();

            var clientEmployees = await _context.ClientEmployees.ToListAsync();

            var result = employees
                .Select(e => (Employee: e, ClientId: clientEmployees.FirstOrDefault(ce => ce.EmployeeId == e.Id)?.ClientId))
                .ToList();

            return result;
        }
    }
}
