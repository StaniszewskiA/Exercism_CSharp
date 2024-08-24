using System;
using System.Text;

public class SimpleCipher
{
    private readonly string Alphabet = "abcdefghijklmnopqrstuvwxyz";
    
    public SimpleCipher()
    {
        StringBuilder sb = new StringBuilder();
        Random rand = new Random();
        for(int i = 0; i < 100; i++)
        {
            sb.Append(Alphabet[rand.Next(0, 26)]);
        }
        Key = sb.ToString();
    }

    public SimpleCipher(string key) => this.Key = key;
    
    public string Key {get; set; }

    public string Encode(string plaintext)
    {
        StringBuilder result = new StringBuilder();
        int distance;
        ExtendKey(plaintext);
        for(int i = 0; i < plaintext.Length; i++)
        {
            distance = (Alphabet.IndexOf(plaintext[i]) + Alphabet.IndexOf(Key[i])) % 26;
            result.Append(Alphabet[distance]);
        }
        
        return result.ToString();
    }

    public string Decode(string ciphertext)
    {
        StringBuilder result = new StringBuilder();
        int distance;
        int tempDistance;
        ExtendKey(ciphertext);
        for(int i = 0; i < ciphertext.Length; i++)
        {
            tempDistance = (Alphabet.IndexOf(ciphertext[i]) - Alphabet.IndexOf(Key[i]));
            distance = tempDistance < 0 ? tempDistance + 26 : tempDistance;
            result.Append(Alphabet[distance]);
        }
        
        return result.ToString();
    }

    private void ExtendKey(string text)
    {
        StringBuilder newKey = new StringBuilder(Key);
        while(newKey.Length < text.Length)
        {
            newKey.Append(Key);
        }
        Key = newKey.ToString();
    }
}