using System;
using System.Collections.Generic;
using System.Linq;

public static class SaddlePoints
{
    public static IEnumerable<(int, int)> Calculate(int[,] matrix)
    {
        int numRows = matrix.GetLength(0);
        int numCols = matrix.GetLength(1);

        int[] rowMax = Enumerable.Range(0, numRows)
            .Select(row => Enumerable.Range(0, numCols)
                   .Max(col => matrix[row, col]))
                    .ToArray();

        int[] colMin = Enumerable.Range(0, numCols)
            .Select(col => Enumerable.Range(0, numRows)
                    .Min(row => matrix[row, col]))
                    .ToArray();

        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                if (rowMax[row] == colMin[col]) yield return (row + 1, col + 1);
            }
        }
    }
}
