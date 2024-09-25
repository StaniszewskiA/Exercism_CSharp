using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reactive;
using System.Reactive.Subjects;

public class HangmanState
{
    public string MaskedWord { get; }
    public ImmutableHashSet<char> GuessedChars { get; }
    public int RemainingGuesses { get; }

    public HangmanState(string maskedWord, ImmutableHashSet<char> guessedChars, int remainingGuesses)
    {
        MaskedWord = maskedWord;
        GuessedChars = guessedChars;
        RemainingGuesses = remainingGuesses;
    }
}

public class TooManyGuessesException : Exception
{
}

public class Hangman
{
    private const char HidingChar = '_';
    private const int MaxGuessCount = 9;

    public IObservable<HangmanState> StateObservable { get; }
    public IObserver<char> GuessObserver { get; }
  
    public Hangman(string word)
    {
        var stateSubject =
            new BehaviorSubject<HangmanState>(
                new HangmanState(MaskedWord(word, new HashSet<char>()),
                ImmutableHashSet<char>.Empty,
                MaxGuessCount
            )
        );
        StateObservable = stateSubject;
        GuessObserver = Observer.Create<char>(x =>
                                              {
                                                  HashSet<char> guessedChars = new HashSet<char>(stateSubject.Value.GuessedChars);
                                                  bool isHit = !guessedChars.Contains(x) && word.Contains(x);
                                                  guessedChars.Add(x);
                                                  string maskedWord = MaskedWord(word, guessedChars);
                                                  if (maskedWord == word)
                                                      stateSubject.OnCompleted();
                                                  else if (stateSubject.Value.RemainingGuesses < 1)
                                                      stateSubject.OnError(new TooManyGuessesException());
                                                  else
                                                      stateSubject.OnNext(new HangmanState(maskedWord, guessedChars.ToImmutableHashSet(),
                                                        isHit ? stateSubject.Value.RemainingGuesses : stateSubject.Value.RemainingGuesses - 1));
                                              });
    }

    private static string MaskedWord(string word, HashSet<char> guessedChars)
        => string.Concat(word.Select(y => guessedChars.Contains(y) ? y : HidingChar));
}
