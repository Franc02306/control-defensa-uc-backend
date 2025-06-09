using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.DTOs.Request
{
    public class ProfessorNewRequest
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

        [Required(ErrorMessage = "El campo Área es obligatorio")]
        public string Area { get; set; }

        [Required(ErrorMessage = "Se debe saber si el profesor Salió al Extranjero")]
        public bool WentAbroad { get; set; }

        [Required(ErrorMessage = "El campo Categoría Docente es obligatorio")]
        public string AcademicRank { get; set; }

        [Required(ErrorMessage = "El campo Categoría Científica es obligatorio")]
        public string ScientificCategory { get; set; }

        // Relacion DireccionRequest
        [Required(ErrorMessage = "El campo Dirección es obligatorio")]
        public AddressNewRequest Address { get; set; }
    }
}
