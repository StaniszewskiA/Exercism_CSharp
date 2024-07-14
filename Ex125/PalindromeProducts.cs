using System;
using System.Collections.Generic;
using System.Linq;

public static class PalindromeProducts
{
    private static bool IsPalindrome(int number)
    {
        var numberAsString = number.ToString();
        return numberAsString.SequenceEqual(numberAsString.Reverse());
    }

    public static (int, IEnumerable<(int,int)>) Largest(int minFactor, int maxFactor)
    {
        int largestPalindrome = int.MinValue;
        var largestFactors = new List<(int, int)>();

        for (int i = minFactor; i <= maxFactor; i++)
        {
            for (int j = i; j = maxFactor; j++)
            {
                int product = i * j;
                if (IsPalindrome(product))
                {
                    if (product > largestPalindrome)
                    {
                        largestPalindrome = product;
                        largestFactors.Clear();
                        largestFactors.Add((i, j));

                    }
                    else if (product == largestPalindrome)
                    {
                        largestFactors.Add((i, j));
                    }
                }
            }
        }

        if (largestPalindrome == int.MinValue)
        {            
            throw new ArgumentException("No palindromic products found in the given range.")
        }

        return (largestPalindrome,   largestFactors)
    }

    public static (int, IEnumerable<(int,int)>) Smallest(int minFactor, int maxFactor)
    {
        int smallestPalindrome = int.MaxValue;
        var samllestFactors = new List<(int, int)>();

        for (int i = minFactor; i <= maxFactor; i++)
        {
            for (int j = i; j <= maxFactor; j++)
            {
                int product = i * j;
                if (IsPalindrome(product))
                {
                    if (product < smallestPalindrome)
                    {
                        smallestPalindrome = product;
                        smallestFactors.Clear();
                        smallestFactors.Add((i, j));
                    }
                    else if (product == smallestPalindrome)
                    {
                        smallestFactors.Add((i, j));
                    }
                }
            }
        }

        if (smallestPalindrome == int.MaxValue)
        {
            throw new ArgumentException("No palindromic products found in the given range.");
        }

        return (smallestPalindrome, smallestFactors);
    }
}
