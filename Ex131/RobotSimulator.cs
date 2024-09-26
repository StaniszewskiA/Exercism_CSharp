using System;

public enum Direction
{
    North = 0,
    East = 1,
    South = 2,
    West = 3
}

public class RobotSimulator
{
    public RobotSimulator(Direction direction, int x, int y)
    {
        Direction = direction;
        X = x;
        Y = y;
    }

    public Direction Direction { get; set; }
    public int X;
    public int Y;

    public void Move(string instructions)
    {
        foreach (var instruction in instructions)
        {
            switch (instruction)
            {
                case 'A':
                    Advance();
                    break;
                case 'R':
                    TurnRight();
                    break;
                case 'L':
                    TurnLeft();
                    break;
                default:
                    throw new ArgumentException("Invalid instruction");
            }
        }
    }

    public void Advance()
    {
        switch (Direction)
        {
            case Direction.North:
                Y++;
                break;
            case Direction.South:
                Y--;
                break;
            case Direction.East:
                X++;
                break;
            case Direction.West:
                X--;
                break;
            default:
                throw new ArgumentException("No direction given");
        }
    }

    public void TurnLeft() => Direction = (Direction)((int)(Direction - 1) < 0 ? 3 : (int)(Direction - 1));
    public void TurnRight() => Direction = (Direction)((int)(Direction + 1) % 4);
}