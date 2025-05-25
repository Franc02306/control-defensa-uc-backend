using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.DTOs.Response
{
    public class ProfessorGetResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Area { get; set; }
        public bool WentAbroad { get; set; }
        public string AcademicRank { get; set; }
        public string ScientificCategory { get; set; }

        // Datos de la dirección que ya viene en SP
        public int AddressId { get; set; }
        public string Province { get; set; }
        public string Municipality { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
    }
}
