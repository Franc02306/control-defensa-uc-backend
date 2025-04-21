using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.Models
{
    [Table("UC_USER", Schema = "dbo")]
    public class User
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("FIRST_NAME")]
        public string FirstName { get; set; }

        [Column("LAST_NAME")]
        public string LastName { get; set; }

        [Column("USERNAME")]
        public string Username{ get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }

        [Column("STATUS")]
        public bool Status { get; set; }

        [Column("PASSWORD")]
        public string Password { get; set; }

        [Column("CREATED_AT")]
        public DateTime CreatedAt { get; set; }
    }
}
