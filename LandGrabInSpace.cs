using System;
using System.Collections.Generic;
using System.Linq;

public struct Coord
{
    ushort X { get; }
    ushort Y { get; }
    public Coord(ushort x, ushort y)
    {
        X = x;
        Y = y;
    }
    
    public double DistanceBetweenCoordsSquared(Coord other) => 
        Math.Pow( (this.X - other.X), 2) + 
        Math.Pow( (this.Y - other.Y), 2);
}

public struct Plot
{
    Coord Coord1 { get; }
    Coord Coord2 { get; }
    Coord Coord3 { get; }
    Coord Coord4 { get; }
    public double LongestSideSquared { get; }
    
    public Plot(Coord coord1, Coord coord2, Coord coord3, Coord coord4)
    {
        Coord1 = coord1;
        Coord2 = coord2;
        Coord3 = coord3;
        Coord4 = coord4;
        LongestSideSquared = CalculateLongestSideSquared(Coord1, Coord2, Coord3, Coord4);
    }
    
    static double CalculateLongestSideSquared(Coord coord1, Coord coord2, Coord coord3, Coord coord4)
    {
        var sides = new double[4];
        sides[0] = coord1.DistanceBetweenCoordsSquared(coord2);
        sides[1] = coord2.DistanceBetweenCoordsSquared(coord3);
        sides[2] = coord3.DistanceBetweenCoordsSquared(coord4);
        sides[3] = coord4.DistanceBetweenCoordsSquared(coord1);
        return sides.Max();
    }
}


public class ClaimsHandler
{
    readonly List<Plot> _listOfPlotsClaimed = new();
    public void StakeClaim(Plot plot) => _listOfPlotsClaimed.Add(plot);

    public bool IsClaimStaked(Plot plot) => _listOfPlotsClaimed.Contains(plot);

    public bool IsLastClaim(Plot plot) => _listOfPlotsClaimed.Last().Equals(plot);

    public Plot GetClaimWithLongestSide() =>
        _listOfPlotsClaimed.OrderByDescending(plot => plot.LongestSideSquared).First();
}
