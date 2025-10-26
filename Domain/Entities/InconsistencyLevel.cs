using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("tblNivelInconsistencia", Schema = "param")]
    public class InconsistencyLevel
    {
        [Key]
        [Column("intIdNivelInconsistencia")]
        public int Id { get; set; }
        [Column("strDescripcion")]
        public string Description { get; set; }
    }
}
