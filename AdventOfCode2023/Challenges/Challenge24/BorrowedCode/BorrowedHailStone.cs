using AdventOfCode2023.Domain;
using AdventOfCode2023.Domain.Geometries;
using MoreLinq;

namespace AdventOfCode2023.Challenges.Challenge04.BorrowedCode;

public static class Borrowed
{

    private readonly record struct BorrowedHailStone(Point3 Position, Velocity3 Velocity)
    {
        public Slope Slope => Velocity.X == 0 ? new Slope(Double.NaN) : new Slope((double)Velocity.Y / Velocity.X);
    }

    private readonly record struct BorrowedIntersection(PointD Point, Time Time);

    static Option<BorrowedIntersection> Intersects(BorrowedHailStone a, BorrowedHailStone b)
    {
        if (a.Slope.IsNaN || b.Slope.IsNaN || a.Slope == b.Slope) return default;

        var c = a.Position.Y - a.Slope * a.Position.X;
        var otherC = b.Position.Y - b.Slope * b.Position.X;

        var x = (otherC - c) / (a.Slope - b.Slope);
        var t1 = (x - a.Position.X) / a.Velocity.X;
        var t2 = (x - b.Position.X) / b.Velocity.X;

        if (t1 < 0 || t2 < 0) return default;

        var y = a.Slope * (x - a.Position.X) + a.Position.Y;
        return new BorrowedIntersection(new PointD(x, y), new (t1));
    }

    public static int Part1(Seq<HailStone> hailstones, Boundary boundary) =>
        hailstones
            .Map(h => new BorrowedHailStone(h.Position, h.Velocity))
            .CartesianPairs()
            .Filter(p => p.first != p.second)
            .Map(p => IntersectionModule.Intersection(
                    new Ray(p.first.Position.ToPoint2(), p.first.Velocity.ToVelocity2()),
                    new Ray(p.second.Position.ToPoint2(), p.second.Velocity.ToVelocity2()))
            )
                
                //Intersects(p.first, p.second))
            .Somes()
            .Count(i => boundary.Contains(i.Point));
}