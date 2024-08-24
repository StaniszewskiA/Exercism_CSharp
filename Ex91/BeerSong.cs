using System;
using System.Linq;

public static class BeerSong
{
    public static string Recite(int startBottles, int takeDown) => Enumerable.Range(startBottles - takeDown + 1, takeDown)
        .Reverse()
        .Select(bottle =>
               bottle switch {
                   int bot when bot > 2 => $"{bot} bottles of beer on the wall, {bot} bottles of beer.\nTake one down and pass it around, {bot - 1} bottles of beer on the wall.",
                   2 => "2 bottles of beer on the wall, 2 bottles of beer.\nTake one down and pass it around, 1 bottle of beer on the wall.",
                   1 => "1 bottle of beer on the wall, 1 bottle of beer.\nTake it down and pass it around, no more bottles of beer on the wall.",
                   _ => "No more bottles of beer on the wall, no more bottles of beer.\nGo to the store and buy some more, 99 bottles of beer on the wall."
               })
        .Aggregate((preLine, postLine) => preLine + "\n\n" + postLine);
}