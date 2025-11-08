using Application.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IRegistroNovedadService
    {
        Task<IEnumerable<RegistroNovedadDto>> GetByEmpleadoIdAsync(Guid empleadoId);
        Task<IEnumerable<RegistroNovedadDto>> GetByPeriodoIdentifierAsync(string identificadorPeriodo);
    }
}
