using System;
using System.Linq;

public static class DifferenceOfSquares
{
    public static int CalculateSquareOfSum(int max)
    {
        int sum = Enumerable.Range(1, max).Sum();
        return sum * sum;
    }

    public static int CalculateSumOfSquares(int max)
    {
        int sumOfSquares = Enumerable.Range(1, max).Select(x => x * x).Sum();
        return sumOfSquares;
    }

    public static int CalculateDifferenceOfSquares(int max) => 
        CalculateSquareOfSum(max) -CalculateSumOfSquares(max);
}