using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assessment.Models
{
    public class Transactions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        [Required]
        public decimal Amount { get; set; }
        public DateTime SentAt { get; set; }

        public int FromAccount { get; set; } //fk
        public int ToAccount { get; set; } // fk
        public Transactions()
        {

        }
    }
}
