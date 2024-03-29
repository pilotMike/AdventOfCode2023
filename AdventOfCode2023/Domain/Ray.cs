namespace AdventOfCode2023.Domain;

public readonly record struct Ray(Point Point, Velocity Velocity)
{
    public InfiniteLine InfiniteLine
    {
        get
        {
            double d = Point.X * Velocity.Y - Point.Y * Velocity.X;
            var p = new Point(-Velocity.Y, Velocity.X);
            return new InfiniteLine(p, new(d));
        }
    }

    public Option<PointD> Intersects(Ray other)
    {
        var l = InfiniteLine;
        var otherl = other.InfiniteLine;

        var infiniteLineIntersection = IntersectionModule.Intersection(l, otherl);

        var current = this;
        return infiniteLineIntersection.Where(i =>
            current.Contains(i) && other.Contains(i));
    }
    
    public bool Contains(Point p, double tolerance = 1e-11)
    {
        var l = InfiniteLine;
        if (l.Contains(p, tolerance))
        {
            var d = l.DistanceAlong(p);
            var da = l.DistanceAlong(this.Point);
            return d <= da;
        }

        return false;
    }
    
    public bool Contains(PointD p, double tolerance = 1e-11)
    {
        var l = InfiniteLine;
        if (l.Contains(p, tolerance))
        {
            var d = l.DistanceAlong(p);
            var da = l.DistanceAlong(this.Point);
            return d <= da;
        }

        return false;
    }
}