using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.DTOs.Request
{
    public class StudentRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Major { get; set; }
        public int Year { get; set; }
        public decimal TeacherAverage { get; set; }

        // Relacion DireccionRequest
        public AddressRequest Address { get; set; }
    }
}
