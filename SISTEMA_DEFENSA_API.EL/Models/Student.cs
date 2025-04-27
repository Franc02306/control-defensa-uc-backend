using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.Models
{
    [Table("UC_STUDENT", Schema = "dbo")]
    public class Student
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("FIRST_NAME")]
        public string FirstName { get; set; }

        [Column("LAST_NAME")]
        public string LastName { get; set; }

        [Column("GENDER")]
        public char Gender { get; set; }

        [Column("BIRTH_DATE")]
        public DateTime BirthDate { get; set; }

        [Column("MAJOR")]
        public string Major { get; set; }

        [Column("YEAR")]
        public int Year { get; set; }

        [Column("TEACHER_AVERAGE")]
        public decimal TeacherAverage { get; set; }

        [Column("ID_ADDRESS")]
        public int IdAddress { get; set; }

        // Relación con Address
        [ForeignKey("IdAddress")]
        public Address? Address { get; set; }
    }
}
