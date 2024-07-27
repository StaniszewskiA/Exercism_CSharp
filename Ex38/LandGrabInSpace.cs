using System;
using System.Collections.Generic;
using System.Linq;

public struct Coord
{
    public Coord(ushort x, ushort y)
    {
        X = x;
        Y = y;
    }

    public ushort X { get; }
    public ushort Y { get; }

    public double DistanceBetweenCoordsSquared(Coord other) =>
        Math.Pow((this.X - other.X), 2) + Math.Pow((this.Y - other.Y), 2);
}

public struct Plot
{
    public Plot(Coord coord1, Coord coord2, Coord coord3, Coord coord4)
    {
        Coord1 = coord1;
        Coord2 = coord2;
        Coord3 = coord3;
        Coord4 = coord4;
        LongestSideSquared = CalculateLongestSideSquared(coord1, coord2, coord3, coord4);
    }

    public Coord Coord1 { get; }
    public Coord Coord2 { get; }
    public Coord Coord3 { get; }
    public Coord Coord4 { get; }
    public double LongestSideSquared { get; }

    private static double CalculateLongestSideSquared(Coord coord1, Coord coord2, Coord coord3, Coord coord4)
    {
        var _sides = new double[4];
        _sides[0] = coord1.DistanceBetweenCoordsSquared(coord2);
        _sides[1] = coord2.DistanceBetweenCoordsSquared(coord3);
        _sides[2] = coord3.DistanceBetweenCoordsSquared(coord4);
        _sides[3] = coord4.DistanceBetweenCoordsSquared(coord1);

        return _sides.Max();
    }
}


public class ClaimsHandler
{
    private List<Plot> _plots = new();
    
    public void StakeClaim(Plot plot) 
    {
        if (!IsClaimStaked(plot)) _plots.Add(plot);
    }

    public bool IsClaimStaked(Plot plot) => _plots.Contains(plot);

    public bool IsLastClaim(Plot plot) => plot.Equals(_plots.Last());

    public Plot GetClaimWithLongestSide() =>
        _plots.OrderByDescending(plot => plot.LongestSideSquared).First();
}
