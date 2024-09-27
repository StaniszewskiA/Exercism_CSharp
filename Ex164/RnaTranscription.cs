using System;
using System.Linq;

public static class RnaTranscription
{
    public static string ToRna(string strand) => new(strand.Select(x => x switch
    {
        'G' => 'C',
        'C' => 'G',
        'T' => 'A',
        'A' => 'U',
        _ => throw new ArgumentException(nameof(strand))
    }).ToArray());
}