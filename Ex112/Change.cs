using System;
using System.Collections.Generic;

public static class Change
{
    public static List<int> FindFewestCoins(int[] coins, int target)
    {
        if (target < 0)
            throw new ArgumentException(nameof(target));

        if (target == 0)
            return new List<int>();

        int[] dp = new int[target + 1];
        int[] lastCoin = new int[target + 1];

        Array.Fill(dp, target + 1);
        dp[0] = 0;

        for (int i = 1; i <= target; i++)
        {
            foreach (int coin in coins)
            {
                if (coin <= i)
                {
                    if (dp[i] > dp[i - coin] + 1)
                    {
                        dp[i] = dp[i - coin] + 1;
                        lastCoin[i] = coin;
                    }
                }
            }
        }

        if (dp[target] > target)
            throw new ArgumentException();

        List<int> result = new List<int>();
        int currentTarget = target;
        while (currentTarget > 0)
        {
            int coin = lastCoin[currentTarget];
            result.Add(coin);
            currentTarget -= coin;
        }

        return result;
    }
}