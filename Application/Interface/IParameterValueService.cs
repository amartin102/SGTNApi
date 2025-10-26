using Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IParameterValueService
    {
        Task<IEnumerable<ParameterValueDto>> GetAllAsync();
        Task<ParameterValueDto> GetByIdAsync(Guid id);
        Task<IEnumerable<ParameterValueDto>> GetByParameterIdAsync(Guid parameterId);
        Task<IEnumerable<ParameterValueDto>> GetByClientIdAsync(Guid clientId);
        Task<IEnumerable<ParameterValueDto>> GetByEmployeeIdAsync(Guid employeeId);
        Task<IEnumerable<ParameterValueDto>> GetByClientAndParameterAsync(Guid clientId, Guid parameterId);
        Task<ParameterValueDto> CreateAsync(CreateParameterValueDto createDto);
        Task UpdateAsync(Guid id, UpdateParameterValueDto updateDto);
        Task DeleteAsync(Guid id);
    }
}
