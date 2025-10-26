using Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IMasterParameterService
    {
        Task<IEnumerable<MasterParameterDto>> GetAllAsync();
        Task<MasterParameterDto> GetByIdAsync(Guid id);
        Task<MasterParameterDto> CreateAsync(CreateMasterParameterDto createDto);
        Task UpdateAsync(Guid id, UpdateMasterParameterDto updateDto);
        Task DeleteAsync(Guid id);
    }
}
