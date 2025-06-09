using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.DTOs.Response
{
    public class ProfessorResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public string Area { get; set; }
        public bool WentAbroad { get; set; }
        public string AcademicRank { get; set; }
        public string ScientificCategory { get; set; }

        // Relacion DireccionResponse
        public AddressResponse Address { get; set; }
    }
}
