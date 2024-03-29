namespace AdventOfCode2023.Domain;

public readonly record struct Line(Point Start, Point End)
{
    public bool IsPoint() => Start == End;
    public bool Contains(Point currPoint) =>
        IntersectionModule.Contains(this, currPoint);
    
    public static implicit operator Line(PointRange pr) => new(pr.Start, pr.End);
}

public readonly record struct InfiniteLine(Point Point, Slope Slope)
{
    public double DistanceAlong(Point point)
        => Point.CrossProduct(Point, point)/Math.Sqrt(Point.X*Point.X + Point.Y*Point.Y);
    
    public double DistanceAlong(PointD point)
        => PointD.CrossProduct((PointD)Point, point)/Math.Sqrt(Point.X*Point.X + Point.Y*Point.Y);

    public double DistanceTo(PointD point, bool signed = false)
    {
        var (A, B) = Point;
        var d = A*point.X + B*point.Y + Slope;
        var m = Math.Sqrt( A*A+B*B );

        return signed ? d/m : Math.Abs(d)/m;
    }
    
    public double DistanceTo(Point point, bool signed = false)
    {
        var (A, B) = Point;
        var d = A*point.X + B*point.Y + Slope;
        var m = Math.Sqrt( A*A+B*B );

        return signed ? d/m : Math.Abs(d)/m;
    }
    
    public bool Contains(Point point, double tolerance = 1e-11) => 
        DistanceTo(point) <= tolerance;
    
    public bool Contains(PointD point, double tolerance = 1e-11) => 
        DistanceTo(point) <= tolerance;
}