using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.Models
{
    public class Municipio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdProvincia { get; set; }

        // Relación con Provincia
        public Provincia? Provincia { get; set; }
    }
}
