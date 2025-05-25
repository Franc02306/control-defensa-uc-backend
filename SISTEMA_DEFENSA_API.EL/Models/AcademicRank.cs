using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.Models
{
    [Table("UC_ACADEMICRANK", Schema = "dbo")]
    public class AcademicRank : Complement { }
}
