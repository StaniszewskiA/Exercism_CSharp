using System;

public static class BinarySearch
{
    public static int Find(int[] input, int value)
    {
        int left = 0;
        int right = input.Length - 1;

        while (left <= right)
        {
            int mid = (left + right) / 2;
            if (value == input[mid]) return mid;
            else if (value < input[mid]) right = mid - 1;
            else left = mid + 1;
        }

        return -1;
    }
}