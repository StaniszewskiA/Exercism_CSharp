using System;
using System.Linq;

public static class Grains
{
    public static double Square(int n)
    {
        if (n is <= 0 or > 64) throw new ArgumentOutOfRangeException(nameof(n));

        return Math.Pow(2, n - 1);
    }

    public static double Total() => Enumerable.Range(1, 64).Sum(Square);
}