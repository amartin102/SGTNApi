using Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface ITurnoProgramadoService
    {
        Task<IEnumerable<TurnoProgramadoDto>> GetAllAsync();
        Task<TurnoProgramadoDto> GetByIdAsync(Guid id);
        Task<IEnumerable<TurnoProgramadoDto>> GetByEmployeeIdAsync(Guid employeeId);
        Task<TurnoProgramadoDto> CreateAsync(CreateTurnoProgramadoDto createDto);
        Task UpdateAsync(Guid id, UpdateTurnoProgramadoDto updateDto);
        Task DeleteAsync(Guid id);
    }
}
