using System;
using System.Linq;

public static class Bob
{
    public static string Response(string statement)
    {
        if (statement.IsSilence()) return "Fine. Be that way!";
        else if (statement.IsYellingQuestion()) return "Calm down, I know what I'm doing!";
        else if (statement.IsYelling()) return "Whoa, chill out!";
        else if (statement.IsQuestion()) return "Sure.";
        else return "Whatever.";
    }

    private static bool IsSilence(this string statement) => string.IsNullOrWhiteSpace(statement);

    private static bool IsYellingQuestion(this string statement) => statement.IsQuestion() && statement.IsYelling();

    private static bool IsYelling(this string statement) => statement.Any(c => char.IsLetter(c)) && statement.ToUpper() == statement;

    private static bool IsQuestion(this string statement) => statement.Trim().EndsWith("?");
}