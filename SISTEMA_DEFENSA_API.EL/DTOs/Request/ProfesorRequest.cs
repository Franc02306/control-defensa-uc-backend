using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.DTOs.Request
{
    public class ProfesorRequest
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public char Sexo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool SalioExtranjero { get; set; }
        public string Departamento { get; set; }
        public string CategoriaDocente { get; set; }
        public string CategoriaCientifica { get; set; }

        // Relacion DireccionRequest
        public DireccionRequest Direccion { get; set; }
    }
}
