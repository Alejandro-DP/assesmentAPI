using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assessment.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Range(100000000, 999999999, ErrorMessage = "El número de cuenta debe tener una longitud de 9 dígitos.")]
        public int Accounts { get; set; }
        public decimal Balance { get; set; }
        public int Owner { get; set; }
        public DateTime CreateAt { get; set; }

    }
}
