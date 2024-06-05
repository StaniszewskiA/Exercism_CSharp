using System;
using System.Text;

public static class Identifier
{
    public static string Clean(string identifier)
    {
        if (string.IsNullOrEmpty(identifier)) return string.Empty;
        StringBuilder cleanedIdentifier = new StringBuilder();
        bool capitalizeNext = false;
        
        foreach (char ch in identifier)
        {
            if (ch >= 'α' && ch <= 'ω') continue;
            else if (char.IsLetter(ch) || ch == '_') 
            {
                if (capitalizeNext)
                {
                    cleanedIdentifier.Append(char.ToUpperInvariant(ch));
                    capitalizeNext = false;
                } else {
                    cleanedIdentifier.Append(ch);
                }
            }
            else if (char.IsWhiteSpace(ch)) cleanedIdentifier.Append('_');
            else if (char.IsControl(ch)) cleanedIdentifier.Append("CTRL");
            else if (ch == '-') capitalizeNext = true;
            
        }

        return cleanedIdentifier.ToString();
    }
}
