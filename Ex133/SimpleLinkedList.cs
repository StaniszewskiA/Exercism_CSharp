using System;
using System.Collections;
using System.Collections.Generic;

public class SimpleLinkedList<T> : IEnumerable<T>
{
    private class Node
    {
        public T Value { get; set; }
        public Node Next { get; set; } 
    }

    private Node Head;
    public int Count { get; private set; }

    public SimpleLinkedList() {}
    public SimpleLinkedList(T value) => Push(value);
    public SimpleLinkedList(IEnumerable<T> values)
    {
        foreach (var value in values)
            Push(value);
    }
    
    public void Push(T value)
    {
        var currNode = new Node { Value = value, Next = Head };
        Head = currNode;
        Count++;
    }

    public T Pop()
    {
        if (Count == 0)
            throw new InvalidOperationException("List is empty");

        var result = Head.Value;
        Head = Head.Next;
        Count--;
        return result;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var currNode = Head;
        while (currNode != null)
        {
            yield return currNode.Value;
            currNode = currNode.Next;
        }
    }

    public SimpleLinkedList<T> Revere()
    {
        var values = new List<T>();
        while (Count != 0)
            values.Add(Pop());

        return new SimpleLinkedList<T>(values);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}