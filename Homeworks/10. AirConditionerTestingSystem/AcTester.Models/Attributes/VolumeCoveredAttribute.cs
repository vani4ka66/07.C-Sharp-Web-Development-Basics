namespace AcTester.Models.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using AcTester.Helpers.Utilities;

    public class VolumeCoveredAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int originalValue = (int)value;
            if (originalValue <= 0)
            {
                return new ValidationResult(string.Format(Constants.NonPositiveNumber, "Volume Covered"));
            }

            return ValidationResult.Success;
        }
    }
}
