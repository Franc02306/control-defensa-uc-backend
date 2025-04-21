using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.DTOs.Request
{
    public class LoginRequest
    {
        public string NombreUsuario { get; set; }
        public string Contrasenia { get; set; }
    }
}
