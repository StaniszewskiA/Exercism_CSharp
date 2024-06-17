using System;
using System.Text;
using System.Text.RegularExpressions;

public static class AtbashCipher
{
    public static string Encode(string plainValue)
    {
        string transformed = Transform(plainValue);
        return FormatIntoGroupsOfFive(transformed);
    }

    public static string Decode(string encodedValue) => Transform(encodedValue);

    public static string Transform(string input)
    {
        string cleanInput = Regex.Replace(input.ToLower(), "[^a-z0-9]", "");

        StringBuilder transformed = new StringBuilder();

        foreach (char c in cleanInput)
        {
            if (char.IsLetter(c))
            {
                char transformedChar = (char)('z' - (c - 'a'));
                transformed.Append(transformedChar);
            }
            else if (char.IsDigit(c)) transformed.Append(c);
        }

        return transformed.ToString();
    }

    private static string FormatIntoGroupsOfFive(string input)
    {
        StringBuilder formatted = new StringBuilder();

        for (int i = 0; i < input.Length; i++)
        {
            if (i > 0 && i % 5 == 0) formatted.Append(' ');
            formatted.Append(input[i]);
        }

        return formatted.ToString();
    }
}
