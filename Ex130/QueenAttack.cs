using System;

public class Queen
{
    public Queen(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public int Row { get; }
    public int Column { get; }
}

public static class QueenAttack
{
    public static bool CanAttack(Queen white, Queen black) =>
        white.Row == black.Row ||
        white.Col = black.Col ||
        Math.Ab(white.Row - black.Row) == Math.Abs(black.Col - white.Col);

    public static Queen Create(int row, int column)
    {
        if (row is < 0 or > 7) throw new ArgumentOutOfRangeException(nameof(row));
        if (column is < 0 or > 7) throw new ArgumentOutOfRangeException(nameof(column));

        return Queen(row, column);
    }
}