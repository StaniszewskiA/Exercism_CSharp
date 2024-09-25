using System;
using System.Collections.Generic;

public enum Bucket
{
    One,
    Two
}

public record Result(int Moves, Bucket GoalBucket, int OtherBucket);
public record Buckets(int One, int Two);
public record State(int Moves, Buckets Buckets);

public class TwoBucket
{
    private readonly int bucketOneCap;
    private readonly int bucketTwoCap;
    private readonly Bucket startBucket;
    
    public TwoBucket(int bucketOneCap, int bucketTwoCap, Bucket startBucket)
    {
        this.bucketOneCap = startBucket == Bucket.One ? bucketOneCap : bucketTwoCap;
        this.bucketTwoCap = startBucket == Bucket.One ? bucketTwoCap : bucketOneCap;
        this.startBucket = startBucket;
    }

    public Result Measure(int goal)
    {
        if (goal > bucketOneCap && goal > bucketTwoCap)
            throw new ArgumentException("Goal cannot not be reached", nameof(goal));

        var initialState = new State(Moves: 0, Buckets: new(One: 0, Two: 0));
        var unprocessed = new PriorityQueue<State, int>();

        unprocessed.Enqueue(initialState, initialState.Moves);

        var statesMinMoveCount = new Dictionary<Buckets, int> { [initialState.Buckets] = initialState.Moves };

        while (unprocessed.TryDequeue(out var state, out _))
        {
            if (state.Buckets.One == goal)
                return new(state.Moves, startBucket == Bucket.One ? Bucket.One : Bucket.Two, state.Buckets.Two);
            if (state.Buckets.Two == goal)
                return new(state.Moves, startBucket == Bucket.One ? Bucket.Two : Bucket.One, state.Buckets.One);

            foreach (var newState in Moves(state))
            {
                if (newState.Moves >= statesMinMoveCount.GetValueOrDefault(newState.Buckets, int.MaxValue))
                    continue;
                statesMinMoveCount[newState.Buckets] = newState.Moves;
                unprocessed.Enqueue(newState, newState.Moves);
            }
        }

        throw new ArgumentException("Could not find path");
    }

    private IEnumerable<State> Moves(State state)
    {
        if (state.Buckets.One == 0)
            yield return new(state.Moves + 1, new(bucketOneCap, state.Buckets.Two));

        if (state.Buckets.One > 0 && state.Buckets.Two == 0)
            yield return new(state.Moves + 1, new(state.Buckets.One, bucketTwoCap));

        if (state.Buckets.Two == bucketTwoCap)
            yield return new(state.Moves + 1, new(state.Buckets.One, 0));

        if (state.Buckets.One > 0 && state.Buckets.Two < bucketTwoCap)
        {
            var amount = Math.Min(state.Buckets.One, bucketTwoCap - state.Buckets.Two);
            yield return new(state.Moves + 1, new(state.Buckets.One - amount, state.Buckets.Two + amount));
        }
    }
}
