using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class UpdateMasterParameterDto
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public int DataTypeId { get; set; }
        public string DataOrigin { get; set; }
        public int InconsistencyLevelId { get; set; }
        public string ModifyPermission { get; set; }
        public string ConsultPermission { get; set; }
        public string ModifiedBy { get; set; }
    }
}
