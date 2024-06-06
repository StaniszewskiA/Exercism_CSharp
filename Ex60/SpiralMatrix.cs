using System;

public class SpiralMatrix
{
    public static int[,] GetMatrix(int size)
    {
        int[,] matrix = new int[size, size];
        int num = 1;
        int top = 0;
        int bottom = size - 1;
        int left = 0;
        int right = size - 1;

        while (num <= size * size)
        {
            for (int i = left; i <= right; i++)
                matrix[top, i] = num++;
            top++;

            for (int i = top; i <= bottom; i++)
                matrix[i, right] = num++;
            right--;

            for (int i = right; i >= left; i--)
                matrix[bottom, i] = num++;
            bottom--;

            for (int i = bottom; i >= top; i--)
                matrix[i, left] = num++;
            left++;
        }

        return matrix;
    }
}
