using System;
using System.Collections.Generic;
using System.Linq;

public static class SumOfMultiples
{
    public static int Sum(IEnumerable<int> multiples, int max)
    {
        HashSet<int> uniqueMultiples = new HashSet<int>();

        foreach (int baseValue in multiples)
        {
            for (int i = baseValue; i < max; i+= baseValue)
            {
                uniqueMultiples.Add(i);
            }
        }

        return uniqueMultiples.Sum();
    }
}