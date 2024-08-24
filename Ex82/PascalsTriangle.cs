using System;
using System.Collections.Generic;

public static class PascalsTriangle
{
    public static IEnumerable<IEnumerable<int>> Calculate(int rows)
    {
        int[][] triangle = new int[rows][];
        
        for (int n = 0; n < rows; n++)
        {
            int[] row = new int[n + 1];
            row[0] = row[n] = 1;
            for (int i = 1; i < n; i++)
            {
                row[i] = triangle[n - 1][i] + triangle[n - 1][n - i];
            }
            triangle[n] = row;
        }

        return triangle;
    }

}