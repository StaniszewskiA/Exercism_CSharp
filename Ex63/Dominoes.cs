using System;
using System.Collections.Generic;
using System.Linq;

public static class Dominoes
{
    public static bool CanChain(IEnumerable<(int, int)> dominoes)
    {
        if (dominoes == null || !dominoes.Any()) return true;
        
        var graph = new Dictionary<int, List<int>>();
        foreach (var (a, b) in dominoes) 
        {
            if (!graph.ContainsKey(a)) graph[a] = new List<int>();
            if (!graph.ContainsKey(b)) graph[b] = new List<int>();
            graph[a].Add(b);
            graph[b].Add(a);
        }

        foreach (var kvp in graph)
        {
            if (kvp.Value.Count % 2 != 0) return false;
        }

        var visited = new HashSet<int>();
        void DFS(int node)
        {
            var stack = new Stack<int>();
            stack.Push(node);
            while (stack.Count > 0)
            {
                int current = stack.Pop();
                if (!visited.Contains(current))
                {
                    visited.Add(current);
                    foreach(var neighbor in graph[current])
                    {
                        if (!visited.Contains(neighbor)) stack.Push(neighbor);
                    }
                }
            }
        }

        var startNode = graph.Keys.FirstOrDefault(key => graph[key].Count > 0);
        if (startNode == default(int)) return false;

        DFS(startNode);

        foreach (var kvp in graph)
        {
            if (kvp.Value.Count > 0 && !visited.Contains(kvp.Key)) return false;
        }

        return true;
    }
}