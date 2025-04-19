using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.DTOs.Response
{
    public class DireccionResponse
    {
        public int Id { get; set; }
        public string Provincia { get; set; }
        public string Municipio { get; set; }
        public string? Calle { get; set; }
        public string? Numero { get; set; }
    }
}
