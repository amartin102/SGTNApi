using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class CreateMasterParameterDto
    {
        public string Code { get; set; }
        public int DataTypeId { get; set; }
        public int InconsistencyLevelId { get; set; }
        public string ModifyPermission { get; set; }
        public string ConsultPermission { get; set; }
        public string CreatedBy { get; set; }
    }
}
