using System.Numerics;

namespace AdventOfCode2023.Domain.Geometries;

public readonly record struct Point(long X, long Y) : ISubtractionOperators<Point, Point, Point>
{
    public IEnumerable<Point> GetAdjacentPoints() =>
        new PointRange(this, this).AdjacentPointsWithHorizontalOrientation();

    public static long CrossProduct(Point a, Point b) => a.X * b.Y - a.Y * b.X;
    
    public static Point operator -(Point start, Point end) => new(end.X - start.X, end.Y - start.Y);

    public static explicit operator PointD(Point p) => new(p.X, p.Y);

    public PointD ToPointD() => (PointD)this;

    public double Distance(Point other) =>
        Math.Sqrt(
            Math.Pow(other.X - X, 2) +
            Math.Pow(other.Y - Y, 2)
        );
}

public readonly record struct Point3(long X, long Y, long Z)
{
    public static explicit operator Point(Point3 p3) => new Point(p3.X, p3.Y);

    public static explicit operator Point3((long x, long y, long z) t) => new Point3(t.x, t.y, t.z);

    public Point ToPoint2() => (Point)this;
    public Vector3 ToVector3() => new(X, Y, Z);
}

public readonly record struct PointD(double X, double Y)
{
    public static double CrossProduct(PointD a, PointD b) => a.X * b.Y - a.Y * b.X;

    public static PointD Normalize(PointD p, int decimalPlaces = 3) =>
        new PointD(Math.Round(p.X, decimalPlaces), Math.Round(p.Y, decimalPlaces));
    
    public Distance Distance(PointD other) =>
        Math.Sqrt(
            Math.Pow(other.X - X, 2) +
            Math.Pow(other.Y - Y, 2)
        ).Apply(v => new Distance(v));
}

public readonly record struct Point3D(double X, double Y, double Z)
{
    public Vector3 ToVector3() => new ((float)X, (float)Y, (float)Z);
    public static Vector3 CrossProduct(Point3D a, Point3D b) => Vector3.Cross(a.ToVector3(), b.ToVector3());

    public static Point3D Normalize(Point3D p, int decimalPlaces = 3) =>
        new (Math.Round(p.X, decimalPlaces), Math.Round(p.Y, decimalPlaces), Math.Round(p.Z, decimalPlaces));
    
    public Distance Distance(Point3D other) =>
        Math.Sqrt(
            Math.Pow(other.X - X, 2) +
            Math.Pow(other.Y - Y, 2) +
            Math.Pow(other.Z - Z, 2)
        ).Apply(v => new Distance(v));
}