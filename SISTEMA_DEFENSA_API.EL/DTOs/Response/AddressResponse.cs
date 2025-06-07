using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.DTOs.Response
{
    public class AddressResponse
    {
        public int Id { get; set; }
        public int IdProvince { get; set; }
        public string Province { get; set; }
        public int IdMunicipality { get; set; }
        public string Municipality { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
    }
}
