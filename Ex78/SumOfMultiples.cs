using System;
using System.Linq;
using System.Collections.Generic;

public static class SumOfMultiples
{
    public static int Sum(IEnumerable<int> multiples, int max) =>
        Enumerable.Range(0, max)
            .Where(x => multiples.Any(i => i != 0 && x % i == 0)).Sum();
}