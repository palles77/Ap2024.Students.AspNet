﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Students.Common.Attributes;

public class CapitalLettersOnlyAttribute : ValidationAttribute, IClientModelValidator
{
    public override bool IsValid(object? value)
    {
        bool result = false;
        if (value is string str)
        {
            if (Regex.IsMatch(str, @"^[A-Z\s]+$"))
            {
                result = true;
            }
        }

        return result;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        ValidationResult? result = new ValidationResult($"{validationContext.DisplayName} must contain only capital letters.");
        if (value is string str)
        {
            if (Regex.IsMatch(str, @"^[A-Z\s]+$"))
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
        MergeAttribute(context.Attributes, "data-val-capital-letters-only", "The field must contain only capital letters.");
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
