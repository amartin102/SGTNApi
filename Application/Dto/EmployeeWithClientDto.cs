using System;

namespace Application.Dto
{
    public class EmployeeWithClientDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }
        public int IdentificationTypeId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Cellphone { get; set; }
        public bool Status { get; set; }

        // Cliente asociado (seg�n tblClienteEmpleado). Puede ser null si no hay asociaci�n.
        public Guid? ClientId { get; set; }
    }
}
