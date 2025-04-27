using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.DTOs.Response
{
    public class StudentsGetResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Major { get; set; }
        public int Year { get; set; }
        public decimal TeacherAverage { get; set; }

        // Datos de la dirección que ya viene en SP
        public int AddressId { get; set; }
        public string Province { get; set; }
        public string Municipality { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
    }
}
