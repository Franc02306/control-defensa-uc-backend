using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SISTEMA_DEFENSA_API.EL.Models
{
    [Table("UC_MUNICIPALITY", Schema = "dbo")]
    public class Municipality
    {
        [JsonPropertyName("id")]
        [Column("ID")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        [Column("NAME")]
        public string Name { get; set; }

        [JsonPropertyName("idProvince")]
        [Column("ID_PROVINCE")]
        public int IdProvince { get; set; }

        [ForeignKey(nameof(IdProvince))]
        public Province? Province { get; set; }
    }
}
