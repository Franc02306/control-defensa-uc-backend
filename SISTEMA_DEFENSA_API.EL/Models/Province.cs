using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.Models
{
    [Table("UC_PROVINCE", Schema = "dbo")]
    public class Province
    {
        [Column("ID")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        [Column("NAME")]
        public string Name { get; set; }
    }
}
