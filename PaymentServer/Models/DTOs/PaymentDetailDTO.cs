using System.ComponentModel.DataAnnotations;

namespace PaymentProject.Models.DTOs
{
    public class PaymentDetailDTO
    {
        public int PaymentDetailsID { get; set; }

        [Required(ErrorMessage = "Card owner name is required")]
        [StringLength(100, ErrorMessage = "Card owner name cannot exceed 100 characters")]
        public string CardOwnerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Card number is required")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Card number must be 16 digits")]
        public string CardNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Expiration date is required")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/([0-9]{2})$", ErrorMessage = "Expiration date must be in MM/YY format")]
        public string ExpirationDate { get; set; } = string.Empty;

        [Required(ErrorMessage = "CVC is required")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "CVC must be 3 digits")]
        public string CVC { get; set; } = string.Empty;
    }
} 