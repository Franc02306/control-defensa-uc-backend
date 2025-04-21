using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.DTOs.Request
{
    public class UserRequest
    {
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El campo Apellido es obligatorio")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public string Username { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; }
        public bool Status { get; set; }
        //public DateTime CreatedAt { get; set; }
    }
}
