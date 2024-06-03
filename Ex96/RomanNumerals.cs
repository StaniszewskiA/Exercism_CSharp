using System;

public static class RomanNumeralExtension
{   
    public static string ToRoman(this int value)
    {
        if (value <= 0 || value > 3999)
            throw new ArgumentOutOfRangeException("value", "Input value must be in the range 1-3999.");

        var romanNumerals = new[]
        {
            new { Value = 1000, Numeral = "M" },
            new { Value = 900, Numeral = "CM" },
            new { Value = 500, Numeral = "D" },
            new { Value = 400, Numeral = "CD" },
            new { Value = 100, Numeral = "C" },
            new { Value = 90, Numeral = "XC" },
            new { Value = 50, Numeral = "L" },
            new { Value = 40, Numeral = "XL" },
            new { Value = 10, Numeral = "X" },
            new { Value = 9, Numeral = "IX" },
            new { Value = 5, Numeral = "V" },
            new { Value = 4, Numeral = "IV" },
            new { Value = 1, Numeral = "I" }
        };

        string result = string.Empty;

        foreach (var item in romanNumerals)
        {
            while (value >= item.Value)
            {
                result += item.Numeral;
                value -= item.Value;
            }
        }
        
        return result;
    }
}