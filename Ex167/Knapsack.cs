using System;

public static class Knapsack
{
    public static int MaximumValue(int maximumWeight, (int weight, int value)[] items)
    {
        int n = items.Length;
        int[,] dp = new int[n + 1, maximumWeight + 1];

        for (int i = 1; i <= n; i++)
        {
            for (int w = 1; w <= maximumWeight; w++)
            {
                if (items[i - 1].weight <= w)
                {
                    dp[i, w] = Math.Max(dp[i - 1, w], dp[i - 1, w - items[i - 1].weight] + items[i - 1].value);
                }
                else
                {
                    dp[i, w] = dp[i - 1, w]; 
                }
            }
        }

        return dp[n, maximumWeight];
    }
}
