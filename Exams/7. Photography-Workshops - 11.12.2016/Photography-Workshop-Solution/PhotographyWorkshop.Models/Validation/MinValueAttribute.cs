using System;
using System.ComponentModel.DataAnnotations;

namespace PhotographyWorkshop.Models.Validation
{
    public class MinValueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                int minValue = int.Parse(value.ToString());
                if (minValue < 100)
                {
                    return false;
                }
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
            catch (OverflowException)
            {
                return false;
            }
        }
    }
}
