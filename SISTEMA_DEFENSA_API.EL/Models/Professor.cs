using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.Models
{
    [Table("UC_PROFESSOR", Schema = "dbo")]
    public class Professor
    {
        [Column("ID")]
        public string Id { get; set; }

        [Column("FIRST_NAME")]
        public string FirstName { get; set; }

        [Column("LAST_NAME")]
        public string LastName { get; set; }

        [Column("GENDER")]
        public char Gender { get; set; }

        [Column("BIRTH_DATE")]
        public DateTime BirthDate { get; set; }

        [Column("AREA")]
        public string Area { get; set; }

        [Column("WENT_ABROAD")]
        public bool WentAbroad { get; set; }

        [Column("ACADEMIC_RANK")]
        public string AcademicRank { get; set; }

        [Column("SCIENTIFIC_CATEGORY")]
        public string ScientificCategory { get; set; }

        [Column("ID_ADDRESS")]
        public int IdAddress { get; set; }

        // Relación con Address
        public Address? Address { get; set; }
    }
}
