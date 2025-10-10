using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interface
{
    public  interface IParameterMasterService
    {
            Task<bool> CreateParameter(ParameterMasterDto dto);

            Task<bool> UpdateParameter(ParameterMasterDto dto);

            Task<ParameterMasterDto> GetParameterById(Guid id);        
    }

}
