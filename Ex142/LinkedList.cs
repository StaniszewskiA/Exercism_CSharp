using System;

public class Node<T>
{
    public T Value { get; set; }
    public Node<T> Next { get; set; }
    public Node<T> Prev { get; set; }

    public Node(T value)
    {
        Value = value;
    }
}

public class Deque<T>
{
    private Node<T> head, tail;
    public int Count { get; private set; }
    
    public void Push(T value)
    {
        var newNode = new Node<T>(value);
        if (tail != null) tail.Next = newNode;
        newNode.Prev = tail;
        tail = newNode;
        if (head == null) head = tail;
        Count++;
    }

    public T Pop()
    {
        if (tail == null) throw new InvalidOperationException("Deque is empty.");
        var value = tail.Value;
        tail = tail.Prev;
        if (tail != null) 
            tail.Next = null;
        else 
            head = null;
        Count--;
        
        return value;
    }

    public void Unshift(T value)
    {
        var newNode = new Node<T>(value);
        if (head != null) head.Prev = newNode;
        newNode.Next = head;
        head = newNode;
        if (tail == null) tail = head;
        Count++;
    }

    public T Shift()
    {
        if (head == null) throw new InvalidOperationException("Deque is empty.");
        var value = head.Value;
        head = head.Next;
        if (head != null) 
            head.Prev = null;
        else 
            tail = null;
        Count--;

        return value;
    }
}