using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("tblMaestroParametro", Schema = "param")]
    public class MasterParameter
    {
        [Key]
        [Column("strIdParametro")]
        public Guid Id { get; set; }
        [Column("strCodParametro")]
        public string Code { get; set; }
        [Column("intIdTipoDato")]
        public int DataTypeId { get; set; }
        [Column("intIdNivelInconsistencia")]
        public int InconsistencyLevelId { get; set; }
        [Column("strPermisoModificar")]
        public string ModifyPermission { get; set; }
        [Column("strPermisoConsultar")]
        public string ConsultPermission { get; set; }
        [Column("strUsuarioCreador")]
        public string CreatedBy { get; set; }
        [Column("datFechaCreacion")]
        public DateTime CreationDate { get; set; }
        [Column("strModificadoPor")]
        public string? ModifiedBy { get; set; }
        [Column("datFechaModificacion")]
        public DateTime? ModificationDate { get; set; }

        // Navigation properties
        public virtual ParameterType DataType { get; set; }
        public virtual InconsistencyLevel InconsistencyLevel { get; set; }
        //public virtual ICollection<ParameterValue> ParameterValues { get; set; }
    }
}
