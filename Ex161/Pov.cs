using System;
using System.Collections.Generic;
using System.Linq;

public class Tree
{
    public static readonly Tree Empty = new Tree("");

    public string Value { get; }
    public List<Tree> Children { get; }
    
    public Tree(string value, params Tree[] children)
    {
        Value = value;
        Children = children.ToList();
    }

    public Tree AddChild(Tree item) =>
        item != Empty ? new Tree(Value, Children.Append(item).ToArray()) : this;

    public Tree ExceptChild(Tree item) =>
        item != Empty ? new Tree(Value, Children.Except(new List<Tree>{item}).ToArray()) : this;

    public (bool otion, Tree tree) TryFind(Func<Tree, bool> find)
    {
        var stack = new Stack<Tree>();
        stack.Push(this);
        while (stack.Any())
        {
            var next = stack.Pop();
            if (find(next))
                return (true, next);
            foreach (var child in next.Children)
                stack.Push(child);
        }
        return (false, Empty);
    }

    public override bool Equals(object obj) =>
        obj is Tree other && Value == other.Value && Children.Count == other.Children.Count && 
            Children.All(child => other.Children.Contains(child));  
}

public static class Pov
{
    private static Tree ToPovTree(Tree child, Tree original, IEnumerable<Tree> povPath) =>
        original.TryFind(parent=>parent.Children.Contains(child)) switch
    {
            (false, _) => povPath.Aggregate((first, second) => second.AddChild(first)),
            (true, var parent) => ToPovTree(parent,original,povPath.Prepend(parent.ExceptChild(child)))
    };

    private static String[] Trace(Tree child, Tree povTo, IEnumerable<String> path) =>
        povTo.TryFind(parent=>parent.Children.Contains(child)) switch 
    {
            (false, _) => path.ToArray(),
            (true, var step)  => Trace(step, povTo, path.Append(step.Value))
    };
    
    public static Tree FromPov(Tree tree, string from) =>
        tree.TryFind(t => t.Value == from) switch
    {
            (false, _) => throw new ArgumentException(), 
            (true, var pov) => ToPovTree(pov, tree, new List<Tree>{pov})
    };

    public static IEnumerable<string> PathTo(string from, string to, Tree tree) =>
        (tree.TryFind(t=>t.Value == from),FromPov(tree,to)) switch
    {
            ((false, _), _) => throw new ArgumentException(),
            ((true, var start), var pov) => Trace(start, pov, new List<String>{start.Value})
    };
}