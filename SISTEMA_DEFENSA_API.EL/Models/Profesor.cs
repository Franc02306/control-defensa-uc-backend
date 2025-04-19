using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.Models
{
    public class Profesor
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public char Sexo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Departamento { get; set; }
        public bool SalioExtranjero { get; set; }
        public string CategoriaDocente { get; set; }
        public string CategoriaCientifica { get; set; }
        public int IdDireccion { get; set; }

        // Relación con Direccion
        public Direccion? Direccion { get; set; }
    }
}
