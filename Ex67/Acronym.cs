using System;
using System.Text.RegularExpressions;

public static class Acronym
{
    public static string Abbreviate(string phrase)
    {
        string cleanedPhrase = Regex.Replace(phrase, @"[^a-zA-Z0-9\s-]", "");
        string[] words = cleanedPhrase.Split(new char[] {' ', '-'}, StringSplitOptions.RemoveEmptyEntries);
        string acronym = "";

        foreach (string word in words)
        {
            if (!string.IsNullOrEmpty(word)) acronym += char.ToUpper(word[0]);
        }

        return acronym.ToUpper();
    }
}