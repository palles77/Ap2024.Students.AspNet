using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Students.Common.Attributes;

public class SmallLettersOnlyAttribute : ValidationAttribute, IClientModelValidator
{
    public override bool IsValid(object? value)
    {
        bool result = false;
        if (value is string str)
        {
            if (Regex.IsMatch(str, @"^[a-z\s]+$"))
            {
                result = true;
            }
        }

        return result;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        ValidationResult? result = new ValidationResult($"{validationContext.DisplayName} must contain only small letters.");
        if (value is string str)
        {
            if (Regex.IsMatch(str, @"^[a-z\s]+$"))
            {
                result = ValidationResult.Success;
            }
        }

        return result;
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        MergeAttribute(context.Attributes, "data-val", "true");
        MergeAttribute(context.Attributes, "data-val-small-letters-only", "The field must contain only small letters.");
    }

    private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
    {
        if (attributes.ContainsKey(key))
        {
            return false;
        }

        attributes.Add(key, value);
        return true;
    }
}
