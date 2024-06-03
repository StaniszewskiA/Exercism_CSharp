using System;

public static class Darts
{
    public static int Score(double x, double y)
    {
       double distanceFromCenter = Math.Sqrt(x * x + y * y);

        if (distanceFromCenter <= 1) return 10;
        else if (distanceFromCenter <= 5) return 5;
        else if (distanceFromCenter <= 10) return 1;
        else return 0;
    }
}
