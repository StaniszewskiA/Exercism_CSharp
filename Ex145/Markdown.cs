using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class Markdown
{
    private static readonly Dictionary<string, string> _headers = new()
    {
        {"# ","h1"},
        {"## ","h2"},
        {"### ","h3"},
        {"#### ","h4"},
        {"##### ","h5"},
        {"###### ","h6"},
        {"* ","li"},
        {"","p"}
    };

    private static readonly Dictionary<string, string> _formats = new()
    {
        {"__","strong"},
        {"_","em"}
    };

    private static string ParseHeader(this string markdown)
    {
        var header = _headers.First(h => markdown.StartsWith(h.Key));
        return $"<{header.Value}>{markdown[header.Key.Length..]}</{header.Value}>";
    }

    private static string ParseFormats(this string markdown)
    {
        foreach (var format in _formats)
            markdown = Regex.Replace(markdown, $"{format.Key}(.+){format.Key}", $"<{format.Value}>$1</{format.Value}>");

        return markdown;
    }

    private static string ParseLists(this string markdown) => Regex.Replace(markdown, "(<li>.+</li>)", "<ul>$1</ul>", RegexOptions.Singleline);

    public static string Parse(string markdown) => string.Join("", markdown
                                                              .Split("\n")
                                                              .Select(markdownLine => markdownLine.ParseHeader().ParseFormats()))
                                                              .ParseLists();
}