using System;
using System.Collections.Generic;
using System.Linq;

public static class Forth
{
    private static readonly Dictionary<string, Func<int, int, int>> Operations = new()
    {
        { "+", (a, b) => a + b},
        { "-", (a, b) => a - b},
        { "*", (a, b) => a * b},
        { "/", (a, b) => a / b},
    };
    
    public static string Evaluate(string[] instructions)
    {
        var stack = new Stack<int>();
        var userWords = new Dictionary<string, string>();

        foreach (var line in instructions)
        {
            if (line.StartsWith(':') && line.EndsWith(';'))
            {
                var words = line[1..^1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var head = words[0].ToLower();
                if (int.TryParse(head, out _)) throw new InvalidOperationException("Integer values cannot be used as word definitions.");
                var tail = words[1..]
                    .Select(w => userWords.TryGetValue(w, out var replacement) ? replacement : w)
                    .Aggregate((current, next) => $"{current} {next}");
                userWords[head] = tail;
                continue;
            }

            ParseLine(stack, userWords, line);
        }

        return string.Join(' ', stack.Reverse());
    }

    private static void ParseLine(Stack<int> stack, Dictionary<string, string> userWords, string line)
    {
        int a;
        int b;

        foreach (var e in line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
        {
            switch (e.ToLower())
            {
                case var op when userWords.ContainsKey(op):
                    ParseLine(stack, userWords, userWords[op]);
                    break;

                case var op when Operations.ContainsKey(op):
                    if (stack.Count < 2) throw new InvalidOperationException($"Insufficient operands for operation '{op}' on the stack.");
                    a = stack.Pop();
                    b = stack.Pop();
                    var result = Operations[op](b, a);
                    stack.Push(result);
                    break;

                case "dup":
                    if (stack.Count == 0) throw new InvalidOperationException("Cannot duplicate the top value; the stack is empty.");
                    stack.Push(stack.Peek());
                    break;

                case "drop":
                    if (stack.Count == 0) throw new InvalidOperationException("Cannot drop from the stack; the stack is empty.");
                    stack.Pop();
                    break;

                case "swap":
                    if (stack.Count < 2) throw new InvalidOperationException("Insufficient elements to swap on the stack.");
                    a = stack.Pop();
                    b = stack.Pop();
                    stack.Push(a);
                    stack.Push(b);
                    break;

                case "over":
                    if (stack.Count < 2) throw new InvalidOperationException("Insufficient elements for 'over' operation on the stack.");
                    a = stack.Pop();
                    b = stack.Pop();
                    stack.Push(b);
                    stack.Push(a);
                    stack.Push(b);
                    break;

                default:
                    if (!int.TryParse(e, out var n)) throw new InvalidOperationException($"Undefined word: '{e}'.");
                    stack.Push(n);
                    break;
            }
        }
    }
}