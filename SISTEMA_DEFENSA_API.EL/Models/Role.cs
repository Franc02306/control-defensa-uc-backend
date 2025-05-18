using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.Models
{
    [Table("UC_ROLE", Schema = "dbo")]
    public class Role
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("NAME")]
        public string Name { get; set; }
    }
}
