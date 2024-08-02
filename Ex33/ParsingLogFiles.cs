using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class LogParser
{
    private readonly string validLineRegexPattern = @"^\[(TRC|DBG|INF|WRN|ERR|FTL)\]";
    private readonly string splitLineRegexPattern = @"<[\^*=-]+>";
    private readonly string quotedPasswordRegexPattern = @""".*password.*""";
    private readonly string endOfLineRegexPattern = @"end-of-line\d+";
    private readonly string weakPasswordRegexPattern = @"password\w+";
    
    public bool IsValidLine(string text) => Regex.IsMatch(text, validLineRegexPattern);

    public string[] SplitLogLine(string text) => Regex.Split(text, splitLineRegexPattern);

    public int CountQuotedPasswords(string lines) => Regex.Matches(lines, quotedPasswordRegexPattern, RegexOptions.IgnoreCase).Count;

    public string RemoveEndOfLineText(string line) => Regex.Replace(line, endOfLineRegexPattern, string.Empty);

    public string[] ListLinesWithPasswords(string[] lines)
    {
        var processedLines = new List<string>();
        foreach (string line in lines)
        {
            Match passwordMatch = Regex.Match(line, weakPasswordRegexPattern, RegexOptions.IgnoreCase);
            if (passwordMatch == Match.Empty) processedLines.Add($"--------: {line}");
            else processedLines.Add($"{passwordMatch.Value}: {line}");
        }

        return processedLines.ToArray();
    }
}
