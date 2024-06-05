using System;

public static class RotationalCipher
{
    public static string Rotate(string text, int key)
    {
        char[] result = new char[text.Length];
        key = key % 26;

        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];
            if (char.IsLetter(c))
            {
                char offset = char.IsUpper(c) ? 'A' : 'a';
                result[i] = (char)((c - offset + key) % 26 + offset);
            } 
            else 
            {
                result[i] = c;
            }
        }

        return new string(result);
    }
}