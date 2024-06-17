using System;
using System.Linq;
using System.Collections.Generic;

public static class ScaleGenerator
{
    private readonly static string[] SharpScale = new string[] { "A", "A#", "B", "C","C#","D","D#","E","F","F#","G","G#" };
    private readonly static string[] FlatScale = new string[]  { "A", "Bb", "B", "C", "Db", "D", "Eb", "E", "F", "Gb", "G", "Ab" };
    
    public static string[] Chromatic(string tonic)
    {
        var baseScale = new string[] { "C", "G", "D", "A", "E", "B", "F#", "a", "e", "b", "f#", "c#", "g#", "d#" }.Contains(tonic) ? SharpScale : FlatScale;
        var index = Array.IndexOf(baseScale, tonic);
        
        return baseScale[index..baseScale.Length].Concat(baseScale[0..index]).ToArray();
    }

    public static string[] Interval(string tonic, string pattern)
    {
        var baseScale = new string[] { "C", "G", "D", "A", "E", "B", "F#", "a", "e", "b", "f#", "c#", "g#", "d#" }.Contains(tonic) ? SharpScale : FlatScale;
        tonic = tonic.Length > 1 ? tonic[0].ToString().ToUpper() + tonic[1] : tonic.ToUpper();
        var index = Array.IndexOf(baseScale, tonic);
        var diatonicScale = new List<string> {tonic};

        foreach (var interval in pattern)
        {
            index += Char.IsUpper(interval) ? interval == 'A' ? 3 : 2 : 1;
            index %= baseScale.Length;
            diatonicScale.Add(baseScale[index]);
        }

        return diatonicScale.ToArray();
    }
}