using SISTEMA_DEFENSA_API.EL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.DTOs.Response
{
    public class StudentResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public string Major { get; set; }
        public int Year { get; set; }
        public decimal TeacherAverage { get; set; }

        // Relacion DireccionResponse
        public AddressResponse Address { get; set; }
    }
}
