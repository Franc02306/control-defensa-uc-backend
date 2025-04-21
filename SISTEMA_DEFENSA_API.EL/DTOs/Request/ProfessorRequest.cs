using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.DTOs.Request
{
    public class ProfessorRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public bool WentAbroad { get; set; }
        public string Area { get; set; }
        public string AcademicRank { get; set; }
        public string ScientificCategory { get; set; }

        // Relacion DireccionRequest
        public AddressRequest Address { get; set; }
    }
}
