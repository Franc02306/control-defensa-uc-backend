using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.DTOs.Response
{
    public class ProfesorResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public char Sexo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Departamento { get; set; }
        public bool SalioExtranjero { get; set; }
        public string CategoriaDocente { get; set; }
        public string CategoriaCientifica { get; set; }

        // Relacion DireccionResponse
        public DireccionResponse Direccion { get; set; }
    }
}
