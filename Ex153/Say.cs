using System;
using System.Collections.Generic;

public static class Say
{
    private const long MAX_ITERATOR = 999_999_999_999;
    private static readonly string[] _onesAndTeensNames = new string[] { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
    private static readonly string[] _tensNames = new string[] { "", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
    private static readonly string[] _groupNames = new string[] { "", "thousand", "million", "billion", "trillion" };
    
    public static string InEnglish(long number)
    {
        if (number < 0 || number > MAX_ITERATOR) throw new ArgumentOutOfRangeException();
        if (number == 0) return "zero";

        var digitGroups = DigitGroups(number);
        var english = string.Empty;

        while (digitGroups.Count > 0)
        {
            english += " " + EnglishForGroup(digitGroups.Count - 1, digitGroups.Pop());
        }

        return english.Trim();
    }

    private static string EnglishForGroup(int groupNumber, int digits)
    {
        var english = string.Empty;
        var hundredsSpacer = string.Empty;

        var hundreds = digits / 100;

        if (hundreds > 0)
        {
            english += _onesAndTeensNames[hundreds] + " hundred";
            digits -= hundreds * 100;
            hundredsSpacer = " ";
        }

        if (0 < digits && digits < 20)
        {
            english += hundredsSpacer + _onesAndTeensNames[digits];
        }
        else
        {
            var tens = digits / 10;
            english += hundredsSpacer + _tensNames[tens];
            digits -= tens * 10;
            if (digits > 0) english += "-" + _onesAndTeensNames[digits];
        }

        if (!(String.IsNullOrEmpty(english))) english += " " + _groupNames[groupNumber];

        return english;
    }

    private static Stack<int> DigitGroups(long number)
    {
        var digitGroups = new Stack<int>();
        while (number > 0)
        {
            digitGroups.Push((int)(number % 1000));
            number = number / 1000;
        }
        return digitGroups;
    }
}