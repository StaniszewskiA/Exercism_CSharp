using System;
using System.Text;

public static class RunLengthEncoding
{
    public static string Encode(string input)
    {
        StringBuilder encoded = new StringBuilder();
        int count = 1;

        for (int i = 1; i <= input.Length; i++)
        {
            if (i == input.Length || input[i] != input[i - 1])
            {
                if (count > 1) encoded.Append(count);
                encoded.Append(input[i - 1]);
                count = 1;
            }
            else
            {
                count++;
            }
        }

        return encoded.ToString();
    }

    public static string Decode(string input)
    {
        StringBuilder decoded = new StringBuilder();
        int count = 0;

        foreach (char c in input)
        {
            if (char.IsDigit(c)) count = count * 10 + (c - '0');
            else
            {
                if (count == 0) count = 1;
                for (int i = 0; i < count; i++) 
                {
                    decoded.Append(c);
                }
                count = 0;
            }
        }

        return decoded.ToString();
    }
}
