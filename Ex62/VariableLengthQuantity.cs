using System;
using System.Collections.Generic;
using System.Linq;

public static class VariableLengthQuantity
{
    public static uint[] Encode(uint[] numbers) => numbers.SelectMany(EncodeNumber).ToArray();

    public static uint[] Decode(uint[] bytes)
    {
        var numbers = new List<uint>();
        uint n = 0;
        var isSequenceOpen = false;

        foreach (var b in bytes)
        {
            if ((b & 0x80) == 0x80)
            {
                n = (n << 7) | (b & 0x7F);
                isSequenceOpen = true;
            }
            else
            {
                n = (n << 7) | b;
                numbers.Add(n);
                isSequenceOpen = false;
                n = 0;
            }
        }

        if (isSequenceOpen) throw new InvalidOperationException("Incomplete sequence at the end of byte array.");

        return numbers.ToArray();
    }

    private static IEnumerable<uint> EncodeNumber(uint number)
    {
        Stack<uint> groups = new Stack<uint>();
        var n = number;

        do
        {
            var g = n & 0x7F;
            groups.Push(g);
            n >>= 7;
        }
        while (n > 0);

        while (groups.Count > 1) yield return groups.Pop() | 0x80;
        yield return groups.Pop();
    }
}