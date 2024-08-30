﻿namespace SyncHms.Fias.Entities.Attributes;

[AttributeUsage(AttributeTargets.Property)]
internal class FiasIntegerAttribute : ValidationAttribute
{
    private string? _errorMessage;

    public override bool IsValid(object? value)
    {
        if (value is null)
            return true;

        if (value is float || value is double || value is decimal)
            return IsValidNumber(value.ToString()!);

        if (value is IEnumerable<float> @floats)
            return IsValidEnumerable(@floats.Select(item => item.ToString()));

        if (value is IEnumerable<double> @doubles)
            return IsValidEnumerable(@doubles.Select(item => item.ToString()));

        if (value is IEnumerable<decimal> @decimals)
            return IsValidEnumerable(@decimals.Select(item => item.ToString()));

        if (value is IEnumerable<float?> @optionalFloats)
            return IsValidEnumerable(@optionalFloats.Select(item => item?.ToString()));

        if (value is IEnumerable<double?> @optionalDoubles)
            return IsValidEnumerable(@optionalDoubles.Select(item => item?.ToString()));

        if (value is IEnumerable<decimal?> @optionalDecimals)
            return IsValidEnumerable(@optionalDecimals.Select(item => item?.ToString()));

        return true;
    }

    public override string FormatErrorMessage(string name) => $"The field {name} {_errorMessage}";

    private bool IsValidNumber(string number)
    {
        if (!number.Contains('.'))
            return true;
        
        _errorMessage = "must be an integer.";
        return false;

    }

    private bool IsValidEnumerable(IEnumerable<string?> numbers)
    {
        if (!numbers.Any(item => item != null && item.Contains('.')))
            return true;
        
        _errorMessage = "includes a non-integer.";
        return false;

    }
}
