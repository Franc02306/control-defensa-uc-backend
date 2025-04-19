using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.Models
{
    public class Direccion
    {
        public int Id { get; set; }
        public int IdProvincia { get; set; }
        public int IdMunicipio { get; set; }
        public string? Calle { get; set; }
        public string? Numero { get; set; }

        // Relaciones de navegación
        public Provincia? Provincia { get; set; }
        public Municipio? Municipio { get; set; }
    }
}
