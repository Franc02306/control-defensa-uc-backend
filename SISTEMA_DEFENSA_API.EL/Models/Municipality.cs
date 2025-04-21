using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.Models
{
    [Table("UC_MUNICIPALITY", Schema = "dbo")]
    public class Municipality
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("ID_PROVINCE")]
        public int IdProvince { get; set; }

        // Relación con Province
        public Province? Province { get; set; }
    }
}
