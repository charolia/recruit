using System;
using System.ComponentModel.DataAnnotations;

namespace Recruit.WebApi.Services.Validators
{
    public class ExpiryRangeAttribute : ValidationAttribute
    {
        private readonly int maxYears;

        public ExpiryRangeAttribute(int end)
        {
            this.maxYears = end;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime expiryDate = (DateTime)value;
            if (expiryDate <= DateTime.Now.AddYears(maxYears) && expiryDate >= DateTime.Now)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}
