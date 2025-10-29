using Application.Dto;
using Application.Interface;
using AutoMapper;
using Domain.Entities;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientDto>> GetActiveClientsAsync()
        {
            var clients = await _clientRepository.GetActiveClientsAsync();
            return clients.Select(c => new ClientDto
            {
                Id = c.Id,
                Name = c.Name,
                Nit = c.Nit,
                Address = c.Address,
                Phone = c.Phone,
                Cellphone = c.Cellphone,
                CityId = c.CityId
            });
        }

        public async Task<IEnumerable<EmployeeWithClientDto>> GetActiveEmployeesWithClientAsync()
        {
            var items = await _clientRepository.GetActiveEmployeesWithClientAsync();
            return items.Select(i => new EmployeeWithClientDto
            {
                Id = i.Employee.Id,
                FirstName = i.Employee.FirstName,
                LastName = i.Employee.LastName,
                Identification = i.Employee.Identification,
                IdentificationTypeId = i.Employee.IdentificationTypeId,
                Address = i.Employee.Address,
                Phone = i.Employee.Phone,
                Cellphone = i.Employee.Cellphone,
                Status = i.Employee.Status,
                ClientId = i.ClientId
            });
        }
    }
}
