using System;
using System.Collections.Generic;
using System.Linq;

public class Anagram
{
    private string baseWord;
    private string normalizedBaseWord;
    
    public Anagram(string baseWord)
    {
        this.baseWord = baseWord;
        this.normalizedBaseWord = Normalize(baseWord);
    }

    public string[] FindAnagrams(string[] potentialMatches)
    {
        var anagrams = new List<string>();
        foreach (var word in potentialMatches)
        {
            if (word.Equals(baseWord, StringComparison.OrdinalIgnoreCase)) continue;
            if (Normalize(word) == normalizedBaseWord) anagrams.Add(word);
        }

        return anagrams.ToArray();
    }

    private string Normalize(string word)
    {
        var charArray = word.ToLower().ToCharArray();
        Array.Sort(charArray);

        return new string(charArray);
    }
}