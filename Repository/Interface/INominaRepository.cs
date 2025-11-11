using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface INominaRepository
    {
        Task<IEnumerable<Dictionary<string, object>>> CalcularNominaCompletaAsync(string strIdPeriodo, string strIdCliente);
    }
}
