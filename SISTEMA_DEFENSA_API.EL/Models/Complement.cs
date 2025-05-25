using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.Models
{
    public class Complement
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("NAME")]
        public string Name { get; set; }
    }
}
