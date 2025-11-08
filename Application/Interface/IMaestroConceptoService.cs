using Application.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IMaestroConceptoService
    {
        Task<IEnumerable<ConceptoNovedadDto>> GetAllWithTipoAsync();
    }
}
