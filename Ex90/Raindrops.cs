using System;
using System.Text;

public static class Raindrops
{
    public static string Convert(int number)
    {
        var stringBuilder = new StringBuilder();
        if (number % 3 == 0) stringBuilder.Append("Pling");
        if (number % 5 == 0) stringBuilder.Append("Plang");
        if (number % 7 == 0) stringBuilder.Append("Plong");

        return stringBuilder.Length == 0 ? number.ToString() : stringBuilder.ToString();
    }
}