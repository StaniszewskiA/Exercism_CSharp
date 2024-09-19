using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

public static class Minesweeper
{
    public static U Forward<T, U>(this T data, Func<T, U> fun) => fun(data);
    
    public static string[] Annotate(string[] input)
    {
        var mines = IndexMines(input);
        return input
            .Select((row, r) => row.Select((cell, c) => ReportMines(cell, mines, Cells(r, c))).Forward(string.Concat))
            .ToArray();
    }

    private static HashSet<(int, int)> IndexMines(string[] input) =>
        input
        .SelectMany((row,r)=> row.Select((char m, int c)=>(m, cell:(r,c))).Where(t=>t.m =='*').Select(t=>t.cell))
        .ToHashSet();

    private static HashSet<(int r, int c)> Cells(int row, int col) =>
        Enumerable.Range(row - 1, 3)
            .SelectMany(r => Enumerable.Range(col - 1, 3)
            .Select(c => (r, c)))
            .ToHashSet();

        private static char ReportMines(char cell, HashSet<(int, int)> mines, HashSet<(int, int)> cells) =>
        cell == '*' ? '*' : cells.Intersect(mines).Count().Forward(cnt => cnt == 0 ? ' ' : (char)('0' + cnt));
}
