using System;
using System.Collections.Generic;
using System.Linq;

public static class Etl
{
    public static Dictionary<string, int> Transform(Dictionary<int, string[]> old) =>
        old.SelectMany(kv => kv.Value.Select(v => KeyValuePair.Create(v.ToLowerInvariant(), kv.Key)))
            .OrderBy(kv => kv.Key)
            .ToDictionary(kv => kv.Key, kv => kv.Value);
}