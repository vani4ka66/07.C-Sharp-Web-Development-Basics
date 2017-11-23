using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcTester.Models.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using AcTester.Helpers.Utilities;

    public class ElectricityUsedAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int originalValue = (int)value;
            if (originalValue <= 0)
            {
                return new ValidationResult(string.Format(Constants.NonPositiveNumber, "Electricity Used"));
            }

            return ValidationResult.Success;
        }
    }
}
