using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.Models
{
    [Table("UC_ADDRESS", Schema = "dbo")]
    public class Address
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("ID_PROVINCE")]
        public int IdProvince { get; set; }

        [Column("ID_MUNICIPALITY")]
        public int IdMunicipality { get; set; }

        [Column("STREET")]
        public string? Street { get; set; }

        [Column("NUMBER")]
        public string? Number { get; set; }

        // Relaciones de navegación
        [ForeignKey("IdProvince")]
        public Province? Province { get; set; }

        [ForeignKey("IdMunicipality")]
        public Municipality? Municipality { get; set; }
    }
}
