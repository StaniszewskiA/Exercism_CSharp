using System;

static class LogLine
{
    public static string Message(string logLine)
    {
        int colonIndex = logLine.IndexOf(":");
        if (colonIndex >= 0) {
            string message = logLine.Substring(colonIndex + 1).Trim();
            return message;
        }
        return string.Empty;
    }

    public static string LogLevel(string logLine)
    {
        int startIndex = logLine.IndexOf("[") + 1;
        int endIndex = logLine.IndexOf("]");
        if (startIndex > 0 && endIndex > startIndex){
            string logLevel = logLine.Substring(startIndex, 
                                                endIndex - startIndex).ToLower();
            return logLevel;
        }  
        return string.Empty;
    }

    public static string Reformat(string logLine)
    {
        string message = Message(logLine);
        string logLevel = LogLevel(logLine);

        return $"{message} ({logLevel})";
    }
}
