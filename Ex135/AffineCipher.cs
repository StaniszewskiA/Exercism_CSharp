using System;
using System.Linq;

public static class AffineCipher
{
    const int M = 26;

    private static U Pipe<T, U>(this T x, Func<T,U> f) => f(x);
    
    private static int Modulo(int a, int m) => (a % m + m) % m;
    
    private static char IndexToChar(int i) => (char)(Modulo(i, M) + 'a');

    private static int CharToIndex(char c) => char.ToLower(c) - 'a';

    private static int Gcd(int a, int b) => a == 0 ? b : Gcd(b % a, a);

    private static bool IsNotCoprime(int a) => Gcd(a, M) != 1;

    private static char Transcode(Func<int, char> f, char c) => char.IsDigit(c) ? c : f(CharToIndex(c));

    private static ArgumentException ArgError(int a) => new ArgumentException("a", $"Error: {a} and {M} must be coprime");
    
    public static string Encode(string plainText, int a, int b)
    {
        if (IsNotCoprime(a)) throw ArgError(a);

        char Encrypt(int x) => IndexToChar((a * x + b) % M);

        return plainText
            .Where(c => char.IsLetterOrDigit(c))
            .Select((c, i) => (Transcode(Encrypt, c), i))
            .GroupBy(t => t.i / 5)
            .Select(grp => grp.Select(v => v.Item1).Pipe(string.Concat))
            .Pipe(x => string.Join(' ', x));
    }

    public static string Decode(string cipheredText, int a, int b)
    {
        if (IsNotCoprime(a)) throw ArgError(a);

        var mmi = Enumerable.Range(1, M).Where(n => a * n % M == 1).Single();
        char Decrypt(int y) => IndexToChar(mmi * (y - b) % M);

        return cipheredText
            .Where(c => char.IsLetterOrDigit(c))
            .Select(c => Transcode(Decrypt, c))
            .Pipe(string.Concat);
    }
}
