using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IMaestroConceptoRepository
    {
        Task<IEnumerable<MaestroConceptoNovedad>> GetAllWithTipoAsync();
    }
}
