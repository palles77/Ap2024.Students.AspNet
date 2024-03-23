using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Students.Common.Attributes;

public class CapitalLettersOnlyAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var result = new ValidationResult("The field must contain only capital letters.");
        if (value is string str)
        {
            if (Regex.IsMatch(str, @"^[A-Z\s]+$"))
            {
                result = ValidationResult.Success;
            }
        }

        return result;
    }
}
