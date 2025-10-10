using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("tblMaestroParametro", Schema = "param")]
    public class ParameterMasterEntity
    {
        [Key]
        public Guid strIdParametro { get; set; }

        public string strCodParametro { get; set; }

        public int intIdTipoDato { get; set; }

        public int intIdNivelInconsistencia { get; set; }

        public string strPermisoModificar { get; set; }

        public string strPermisoConsultar { get; set; }

        public string strUsuarioCreador { get; set; }

        public DateTime datFechaCreacion { get; set; }

        public string strModificadoPor { get; set; }

        public DateTime datFechaModificacion { get; set; }

    }
}
