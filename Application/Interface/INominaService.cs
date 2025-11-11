using Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface INominaService
    {
        Task<IEnumerable<CalculoNominaDto>> CalcularNominaAsync(string strIdPeriodo, string strIdCliente);
    }
}
