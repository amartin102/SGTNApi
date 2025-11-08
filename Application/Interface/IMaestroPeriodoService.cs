using Application.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IMaestroPeriodoService
    {
        Task<IEnumerable<MaestroPeriodoDto>> GetByValorParametroPeriodicidadIdAsync(Guid valorParametroId);
        Task<MaestroPeriodoDto> GetByIdAsync(Guid id);
        Task<MaestroPeriodoDto> CreateAsync(CreateMaestroPeriodoDto createDto);
        Task UpdateAsync(Guid id, UpdateMaestroPeriodoDto updateDto);
        Task DeleteAsync(Guid id);
        Task<MaestroPeriodoDto> ApplyPeriodAsync(Guid id, bool force = false);

        // Client-scoped operations
        Task<IEnumerable<MaestroPeriodoDto>> GetByClientIdAsync(Guid clientId);
        Task UpdateRangeForClientAsync(Guid clientId, IEnumerable<MaestroPeriodoDto> updateDtos);
    }
}
