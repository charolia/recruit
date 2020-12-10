using Recruit.WebApi.Services.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace Recruit.WebApi.Services.Models
{
    public class CreditCardDto
    {
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[0-9a-zA-Z]+$", ErrorMessage = "Name contains invalid characters")]
        public string Name { get; set; }
        
        [Required]
        [CreditCard(ErrorMessage = "Invalid Credit Card Number")]
        public string CreditCardNumber { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{4}|[0-9]{3}$", ErrorMessage = "Invalid CVC Number")]
        public string CVC { get; set; }
        
        [ExpiryRange(10, ErrorMessage = "Invalid Expiry Date, should be future dated")]
        public DateTime Expiry { get; set; }
    }
}
