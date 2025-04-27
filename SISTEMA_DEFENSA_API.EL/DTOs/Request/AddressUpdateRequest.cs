using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.DTOs.Request
{
    public class AddressUpdateRequest
    {
        public int? IdProvince { get; set; }
        public int? IdMunicipality { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
    }
}
