using System;
using System.Collections.Generic;

public static class Pangram
{
    public static bool IsPangram(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return false;

        HashSet<char> letters = new HashSet<char>();

        foreach (char c in input.ToLower())
        {
            if (char.IsLetter(c)) letters.Add(c);
        }

        return letters.Count == 26;
    }
}
