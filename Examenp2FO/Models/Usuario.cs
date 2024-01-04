using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Examenp2FO.Models
{
    public class Class
    {
        [Table("ClienteP2")]
        public class ClienteP2
        {
            [Key]
            public int ID { get; set; }

            [Required]
            [StringLength(50)]
            public string Nombre { get; set; }

            [Required]
            [StringLength(50)]
            public string Apellido { get; set; }

            [Required]
            [StringLength(50)]
            public string Usuario { get; set; }

            [Required]
            [StringLength(50)]
            public string Ciudad { get; set; }
        }
    }
}
