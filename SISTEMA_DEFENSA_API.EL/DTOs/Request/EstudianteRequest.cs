using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.DTOs.Request
{
    public class EstudianteRequest
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public char Sexo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Carrera { get; set; }
        public int Anio { get; set; }
        public decimal PromedioDocente { get; set; }

        // Relacion DireccionRequest
        public DireccionRequest Direccion { get; set; }
    }
}
