using System;

public static class ReverseString
{
    public static string Reverse(string input)
    {
        if (input == null) throw new ArgumentNullException(nameof(input), "Input string cannot be null.");

        char[] charArray = new char[input.Length];
        int endIndex = input.Length - 1;

        for (int i = 0; i < input.Length; i++) 
        {
            charArray[i] = input[endIndex - i]            
        }

        return new string(charArray);
    }
}