using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.DTOs.Request
{
    public class AddressNewRequest
    {
        [Required(ErrorMessage = "La Provincia es obligatoria")]
        public int IdProvince { get; set; }

        [Required(ErrorMessage = "El Municipio es obligatoria")]
        public int IdMunicipality { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
    }
}
