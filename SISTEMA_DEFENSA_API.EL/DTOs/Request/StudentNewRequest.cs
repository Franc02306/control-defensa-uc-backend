using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.DTOs.Request
{
    public class StudentNewRequest
    {
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El campo Apellido es obligatorio")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El campo Género es obligatorio")]
        public char Gender { get; set; }

        [Required(ErrorMessage = "El campo Fecha de Nacimiento es obligatorio")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "El campo Edad es obligatorio")]
        public int Age { get; set; }

        [Required(ErrorMessage = "El campo Carrera es obligatorio")]
        public string Major { get; set; }

        [Required(ErrorMessage = "El campo Año es obligatorio")]
        public int Year { get; set; }

        [Required(ErrorMessage = "El campo Promedio Docente es obligatorio")]
        public decimal TeacherAverage { get; set; }

        // Relacion DireccionRequest
        [Required(ErrorMessage = "El campo Dirección es obligatorio")]
        public AddressNewRequest Address { get; set; }
    }
}
