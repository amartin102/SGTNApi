using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("tblClienteEmpleado", Schema = "client")]
    public class ClientEmployee
    {
        [Column("strIdCliente")]
        public Guid ClientId { get; set; }

        [Column("strIdEmpleado")]
        public Guid EmployeeId { get; set; }

        // Navigation properties
        public virtual Client Client { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
