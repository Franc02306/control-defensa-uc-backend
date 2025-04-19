using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.DTOs.Request
{
    public class DireccionRequest
    {
        public int IdProvincia { get; set; }
        public int IdMunicipio { get; set; }
        public string? Calle { get; set; }
        public string? Numero { get; set; }
    }
}
