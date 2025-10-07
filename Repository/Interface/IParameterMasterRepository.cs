using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
   public interface IParameterMasterRepository
    {
        Task<ParameterMasterEntity> CreateParameter(ParameterMasterEntity entity);

        Task<bool> UpdateParameter(ParameterMasterEntity entity);

        Task<ParameterMasterEntity> GetParameterById(Guid id);

    }
}
