using System;

public static class LogAnalysis 
{
    public static string SubstringAfter(this string str, string delimiter) 
    {
        int index = str.IndexOf(delimiter);

        if (index == -1) return string.Empty;

        int startIndex = index + delimiter.Length;

        return str.Substring(startIndex);
    }

    public static string SubstringBetween(this string str, string startDelimiter, string endDelimiter)
    {
        int startIndex = str.IndexOf(startDelimiter);
        if (startIndex == -1) return string.Empty;

        startIndex += startDelimiter.Length;

        int endIndex = str.IndexOf(endDelimiter, startIndex);
        if (endIndex == -1) return string.Empty;

        return str.Substring(startIndex, endIndex - startIndex);
    }
    
    public static string Message(this string str) => str.SubstringAfter(": ");
    public static string LogLevel(this string str) => str.SubstringBetween("[", "]");
}