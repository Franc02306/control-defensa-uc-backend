using SISTEMA_DEFENSA_API.EL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.DTOs.Response
{
    public class EstudianteResponse
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public char Sexo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Carrera { get; set; }
        public int Anio { get; set; }
        public decimal PromedioDocente { get; set; }

        // Relacion DireccionResponse
        public DireccionResponse Direccion { get; set; }
    }
}
