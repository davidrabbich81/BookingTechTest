using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Attributes
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public FutureDateAttribute() { }

        public string GetErrorMessage() => "Date must be in the future";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult(GetErrorMessage());

            var date = (DateTime)value;

            if (DateTime.Compare(date, DateTime.Now) < 0) 
                return new ValidationResult(GetErrorMessage());
            else 
                return ValidationResult.Success;
        }
    }
}
