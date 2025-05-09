using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentProject.Models
{
    public class PaymentDetail
    {
        [Key]
        public int PaymentDetailsID { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string CardOwnerName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "nvarchar(16)")]
        public string CardNumber { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "nvarchar(5)")]
        public string ExpirationDate { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "nvarchar(3)")]
        public string CVC { get; set; } = string.Empty;
    }
}
