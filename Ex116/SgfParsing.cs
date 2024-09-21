using System;
using System.Collections.Generic;
using System.Linq;
using Sprache;

public class SgfTree
{
    public SgfTree(IDictionary<string, string[]> data, params SgfTree[] children)
    {
        Data = data;
        Children = children;
    }

    public IDictionary<string, string[]> Data { get; }
    public SgfTree[] Children { get; }
}

public class SgfParser
{
    public static SgfTree ParseTree(string input)
    {
        var escapedCharParser = from _ in Parse.Char('\\')
                                from c in Parse.AnyChar
                                select c == 'n' || c == 't' ? ' ' : c;
        var propValueParser = from _ in Parse.Char('[')
                              from value in escapedCharParser.Or(Parse.AnyChar.Except(Parse.Char(']'))).Many().Text()
                              from __ in Parse.Char(']')
                              select value;
        var propertyParser = from key in Parse.AtLeastOnce(Parse.Upper).Text()
                             from values in Parse.AtLeastOnce(propValueParser)
                             select (key: key, values: values);
        var nodeParser = from _ in Parse.Char(';')
                         from properties in Parse.Many(propertyParser)
                         select properties.ToDictionary(x => x.key, x => x.values.ToArray());
        var gameTreeParser = default(Parser<SgfTree>);
        gameTreeParser = from _ in Parse.Char('(')
                         from nodes in Parse.AtLeastOnce(nodeParser)
                         from gameTrees in Parse.Many(gameTreeParser)
                         from __ in Parse.Char(')')
                         let nodesInReverse = nodes.Reverse().ToArray()
                         select nodesInReverse.Skip(1).Aggregate(new SgfTree(nodesInReverse[0], gameTrees.ToArray()), (tree, node) => new SgfTree(node, tree));

        var result = gameTreeParser.TryParse(input);
        return result.WasSuccessful ? result.Value : throw new ArgumentException();
    }
}