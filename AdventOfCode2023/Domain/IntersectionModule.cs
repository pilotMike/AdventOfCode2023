using System.Numerics;
using AdventOfCode2023.Challenges.Challenge04;
using AdventOfCode2023.Domain.Geometries;

namespace AdventOfCode2023.Domain;

public static class IntersectionModule
{
    public static bool Contains(Line line, Point currPoint)
    {
        if (line.IsPoint())
        {
            return line.Start == currPoint;
        }
        
        var (point1, point2) = (line.Start, line.End);
        // var dxc = currPoint.X - point1.X;
        // var dyc = currPoint.Y - point1.Y;
        var c = currPoint - point1;

        // var dxl = point2.X - point1.X;
        // var dyl = point2.Y - point1.Y;
        var l = point2 - point1;

        //var cross = dxc * dyl - dyc * dxl;
        var cross = Point.CrossProduct(c, l);
        var (dxl, dyl) = l;

        if (cross != 0) return false;

        bool res;
        if (Math.Abs(dxl) >= Math.Abs(dyl))
            res = dxl > 0 ? 
                point1.X <= currPoint.X && currPoint.X <= point2.X :
                point2.X <= currPoint.X && currPoint.X <= point1.X;
        else
            res = dyl > 0 ? 
                point1.Y <= currPoint.Y && currPoint.Y <= point2.Y :
                point2.Y <= currPoint.Y && currPoint.Y <= point1.Y;

        return res;
    }

    // public static Option<PointD> Intersection(Ray a, Ray b) =>
    //     a.Intersects(b).Map(x=>PointD.Normalize(x));

    public readonly record struct IntersectionData(PointD Point, Time Time);
    // public readonly record struct IntersectionData3(PointD3 Point, Time Time);
    public static Option<IntersectionData> Intersection(Ray a, Ray b)
    {
        if (a.Slope2.IsNaN || b.Slope2.IsNaN || a.Slope2 == b.Slope2) return default;

        var c = a.Point.Y - a.Slope2 * a.Point.X;
        var otherC = b.Point.Y - b.Slope2 * b.Point.X;

        var x = (otherC - c) / (a.Slope2 - b.Slope2);
        var t1 = (x - a.Point.X) / a.Velocity.X;
        var t2 = (x - b.Point.X) / b.Velocity.X;

        if (t1 < 0 || t2 < 0) return default;

        var y = a.Slope2 * (x - a.Point.X) + a.Point.Y;
        return new IntersectionData(new PointD(x, y), new (t1));
    }
    
    public readonly record struct IntersectionData3(Point3D Point, Time Time);

    public static Option<IntersectionData3> Intersection(Ray3 a, Ray3 b) =>
        LineLine(
            a.Point.ToVector3(), a.Velocity.ToVector3(),
            b.Point.ToVector3(), b.Velocity.ToVector3(),
            out var intersection, out float distance)
            ? new IntersectionData3(new Point3D(intersection.X, intersection.Y, intersection.Z), new Time(distance))
            : None;
        
    
    /// <summary>
    /// Computes an intersection of the lines.
    /// copied from https://github.com/Syomus/ProceduralToolkit/blob/48c93f9eb1a629947408d9060fd5175eb5304737/Runtime/Geometry/Intersect3D.cs#L8
    /// </summary>
    private static bool LineLine(Vector3 originA, Vector3 directionA, Vector3 originB, Vector3 directionB,
        out Vector3 intersection, out float distance)
    {
        float sqrMagnitudeA = directionA.SqrMagnitude();
        float sqrMagnitudeB = directionB.SqrMagnitude();
        float dotAB = Vector3.Dot(directionA, directionB);

        float denominator = sqrMagnitudeA*sqrMagnitudeB - dotAB*dotAB;
        Vector3 originBToA = originA - originB;
        float a = Vector3.Dot(directionA, originBToA);
        float b = Vector3.Dot(directionB, originBToA);

        Vector3 closestPointA;
        Vector3 closestPointB;
        if (Math.Abs(denominator) < Geometry.Epsilon)
        {
            // Parallel
            float distanceB = dotAB > sqrMagnitudeB ? a/dotAB : b/sqrMagnitudeB;

            closestPointA = originA;
            closestPointB = originB + directionB*distanceB;

            distance = default;
        }
        else
        {
            // Not parallel
            float distanceA = (sqrMagnitudeA*b - dotAB*a)/denominator;
            float distanceB = (dotAB*b - sqrMagnitudeB*a)/denominator;

            closestPointA = originA + directionA*distanceA;
            closestPointB = originB + directionB*distanceB;

            distance = distanceA;
        }

        if ((closestPointB - closestPointA).SqrMagnitude() < Geometry.Epsilon)
        {
            intersection = closestPointA;
            return true;
        }
        intersection = Vector3.Zero;
        return false;
    }
    
    // public static Option<IntersectionData3> Intersection(Ray3 a, Ray3 b)
    // {
    //     if (a.Slope.IsNaN || b.Slope2.IsNaN || a.Slope2 == b.Slope2) return default;
    //
    //     var c = a.Point.Y - a.Slope2 * a.Point.X;
    //     var otherC = b.Point.Y - b.Slope2 * b.Point.X;
    //
    //     var x = (otherC - c) / (a.Slope2 - b.Slope2);
    //     var t1 = (x - a.Point.X) / a.Velocity.X;
    //     var t2 = (x - b.Point.X) / b.Velocity.X;
    //
    //     if (t1 < 0 || t2 < 0) return default;
    //
    //     var y = a.Slope2 * (x - a.Point.X) + a.Point.Y;
    //     return new IntersectionData(new PointD(x, y), new (t1));
    // }

    public static Option<PointD> Intersection(InfiniteLine a, InfiniteLine b)
    {
        double d = Point.CrossProduct(a.Point, b.Point);

        if (d != 0)
        {
            var point = new PointD(
                (a.Point.Y * b.Slope - b.Point.Y * a.Slope) / d,
                (b.Point.X * a.Slope - a.Point.X * b.Slope) / d);
            return point;
        }

        return None;
    }

    // public static Option<PointD> Intersection(Ray la, Ray lb, double tolerance = 1e-11)
    // {
    //     var (a, b) = (la.Point, lb.Point);
    //     // ax + by = c
    //     if (la.Velocity == lb.Velocity) return None; // same slope, they'll never intersect
    //     
    //     
    //     
    //     
    //     
    //     double delta = Point.CrossProduct(a, b);
    //     
    //     if (delta != 0)
    //     {
    //         float x = (b2 * )
    //         var p = new PointD(
    //             (a.Y * lb.Velocity - b.Y * la.Velocity) / delta,
    //             (b.X * la.Velocity - a.X * lb.Velocity) / delta);
    //
    //         if (la.Contains(p, tolerance)
    //             && lb.Contains(p, tolerance))
    //         {
    //             return p;
    //         }
    //     }
    //
    //     return None;
    // }
}