using System;
using System.Collections.Generic;

public static class NucleotideCount
{
    public static IDictionary<char, int> Count(string sequence)
    {
        var neuclotides_dict = new Dictionary<char, int>
        {
            ['A'] = 0,
            ['C'] = 0,
            ['G'] = 0,
            ['T'] = 0,
        };

        foreach (var nucleotide in sequence)
        {
            if (!neuclotides_dict.ContainsKey(nucleotide)) throw new ArgumentException();
            neuclotides_dict[nucleotide]++;
        }

        return neuclotides_dict;
    }
}