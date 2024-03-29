using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Domain;

public readonly record struct Point(int X, int Y) : ISubtractionOperators<Point, Point, Point>
{
    public IEnumerable<Point> GetAdjacentPoints() =>
        new PointRange(this, this).AdjacentPointsWithHorizontalOrientation();

    public static int CrossProduct(Point a, Point b) => a.X * b.Y - a.Y * b.X;
    
    public static Point operator -(Point start, Point end) => new(end.X - start.X, end.Y - start.Y);

    public static explicit operator PointD(Point p) => new(p.X, p.Y);

    public PointD ToPointD() => (PointD)this;

    public double Distance(Point other) =>
        Math.Sqrt(
            Math.Pow(other.X - X, 2) +
            Math.Pow(other.Y - Y, 2)
        );
}

public readonly record struct Point3(int X, int Y, int Z)
{
    public static explicit operator Point(Point3 p3) => new Point(p3.X, p3.Y);

    public static explicit operator Point3((int x, int y, int z) t) => new Point3(t.x, t.y, t.z);

    public Point ToPoint2() => (Point)this;
}

public readonly record struct PointD(double X, double Y)
{
    public static double CrossProduct(PointD a, PointD b) => a.X * b.Y - a.Y * b.X;

    public static PointD Normalize(PointD p, int decimalPlaces = 3) =>
        new PointD(Math.Round(p.X, decimalPlaces), Math.Round(p.Y, decimalPlaces));
    
    public double Distance(PointD other) =>
        Math.Sqrt(
            Math.Pow(other.X - X, 2) +
            Math.Pow(other.Y - Y, 2)
        );
}