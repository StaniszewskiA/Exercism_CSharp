using System;
using System.Collections.Generic;
using System.Linq;

public static class NthPrime
{
    public const int SMALL_INT = 6;
    public const int LOWER_BOUND = 15;
    
    public static int Prime(int nth)
    {
        if (nth < 1)
            throw new ArgumentOutOfRangeException(nameof(nth));

        int upperBound = EstimateUpperBound(nth);
        var primes = Sieve(upperBound);

        return primes.ElementAt(nth - 1);
    }

    private static List<int> Sieve(int upperBound)
    {
        bool[] sieve = new bool[upperBound + 1];
        for (int i = 2; i <= upperBound; i++)
            sieve[i] = true;

        for (int i = 2; i * i <= upperBound; i++)
        {
            if (sieve[i])
                for (int j = i * i; j <= upperBound; j += i)
                    sieve[j] = false;
        }

        var primes = new List<int>();
        for (int i = 2; i <= upperBound; i++)
            if (sieve[i])
                primes.Add(i);

        return primes;
    }

    private static int EstimateUpperBound(int nth)
    {
        if (nth < SMALL_INT)
            return LOWER_BOUND;

        return (int)(nth * (Math.Log(nth) + Math.Log(Math.Log(nth))));
    }
}