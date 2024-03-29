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

    public static Option<PointD> Intersection(Ray a, Ray b) =>
        a.Intersects(b).Map(x=>PointD.Normalize(x));

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