using System.Diagnostics;
using AdventOfCode2023.Challenges.Challenge04.BorrowedCode;
using AdventOfCode2023.Domain;

namespace AdventOfCode2023.Challenges.Challenge04;

public readonly record struct HailStone(Point3 Position, Velocity3 Velocity) : IPointVelocity3;

public readonly record struct HailStone2(Point Position, Velocity Velocity) : IPointVelocity2;

[DebuggerDisplay("{this.HailStone}\nOther: {this.ClosestIntersection.Map(x => x.Other.HailStone)}")]
public class HailStoneIntersection(HailStone2 hailStone) 
{
    public readonly record struct IntersectionInfo(HailStoneIntersection Other, PointD Point, Distance Distance)
    {
        public static implicit operator IntersectionInfo((HailStoneIntersection Other, PointD Point, Distance Distance) t) =>
            new(t.Other, t.Point, t.Distance);
    }
    
    public HailStone2 HailStone { get; } = hailStone;
    

    private SortedList<Distance, IntersectionInfo>? _collisions;
    private SortedList<Distance, IntersectionInfo> Collisions => _collisions ??= new();

    private SortedList<Distance, IntersectionInfo>? _outOfBounds;
    private SortedList<Distance, IntersectionInfo> OutOfBounds => _outOfBounds ??= new();

    
    public Option<IntersectionInfo> FirstInBoundsCollision => Collisions.HeadOrNone().Map(x => x.Value);

    public Option<IntersectionInfo> FirstCollision =>
        (Collisions.HeadOrNone() | OutOfBounds.HeadOrNone()).Map(x => x.Value);

    public IntersectionInfo AddCollision(HailStoneIntersection other, PointD intersectionPoint)
    {
        var distance = other.HailStone.Position.ToPointD().Distance(intersectionPoint);
        
        Collisions.Add(distance, (other, intersectionPoint, distance));
        return (other, intersectionPoint, distance);
    }

    public IntersectionInfo AddOutsideBoundaryCollision(HailStoneIntersection other,
        PointD intersectionPoint)
    {
        var distance = other.HailStone.Position.ToPointD().Distance(intersectionPoint);
        
        OutOfBounds.Add(distance, (other, intersectionPoint, distance));
        return (other, intersectionPoint, distance);
    }

    public Option<(IntersectionInfo intersectionInfo, bool isInBounds, int order)> IntersectsWith(HailStoneIntersection other) =>
        Collisions.Values.FindIndex(x => other == x.Other).Map(x => (x.item, true, x.index)) |
        OutOfBounds.Values.FindIndex(x => other == x.Other).Map(x => (x.item, false, x.index));
}

public class HailStoneCollisionEqualityComparer : IEqualityComparer<HailStoneIntersection>
{
    public bool Equals(HailStoneIntersection? x, HailStoneIntersection? y) =>
        (x == null || y == null) ||
        x.HailStone == y.HailStone ||
        y.FirstInBoundsCollision.Map(yy => yy.Other == x).IfNone(false) ||
        x.FirstInBoundsCollision.Map(xx => xx.Other == y).IfNone(false);

    public int GetHashCode(HailStoneIntersection obj) => 
        obj.HailStone.GetHashCode() ^ obj.FirstInBoundsCollision.Map(x => x.Other.HailStone.GetHashCode()).IfNone(0);
}

public static class Parts
{
    public static Boundary TestBoundary => new Boundary(
        new NumberRange(
            new(200000000000000, true),
            new(400000000000000, true)),
        new NumberRange(
            new(200000000000000, true),
            new(400000000000000, true))
    );
    
    public static int Part1(IChallenge24Input input, Boundary? boundary = null) =>
        new Parser().Parse(input)
            .Map(hs => new HailStoneIntersection(hs.ToHailStone2()))
            .Apply(hsi => MapIntersections(hsi, boundary ?? TestBoundary))
            //.Distinct(new HailStoneCollisionEqualityComparer())
            .Count(hsi => hsi.FirstCollision.IsSome);

    public static int Part1Borrowed(IChallenge24Input input, Boundary? boundary = null) =>
        new Parser().Parse(input)
            .Apply(hs => Borrowed.Part1(hs, boundary ?? TestBoundary));

    public static int Part1_A(IChallenge24Input input, Boundary? boundary = null) =>
        new Parser().Parse(input)
            .CartesianPairs()
            .Filter(p => p.first != p.second)
            .Map(p => IntersectionModule.Intersection(p.first.ToHailStone2().ToRay(), p.second.ToHailStone2().ToRay()))
            .Somes()
            .Count(i => (boundary ?? TestBoundary).Contains(i.Point));
    
    // public static int Part2_A(IChallenge24Input input, Boundary? boundary = null) =>
    //     new Parser().Parse(input)
    //         .CartesianPairs()
    //         .Filter(p => p.first != p.second)
    //         .Map(p => IntersectionModule.Intersection(p.first.ToRay3(), p.second.ToRay3()))
    //         .Somes()
    //         .Count(i => (boundary ?? TestBoundary).Contains(i.Point));

    /// Set the closest intersection points for each piece of hail
    public static Seq<HailStoneIntersection> MapIntersections(Seq<HailStoneIntersection> hsi, Boundary boundary)
    {
        // this one is way wrong
        var intersections =
            from ta in hsi.Map((i, x) => (i,x))
            from b in hsi.Skip(ta.i + 1)
            let a = ta.x
            from intersection in IntersectionModule.Intersection(a.HailStone.ToRay(), b.HailStone.ToRay())
            let _ = boundary.Contains(intersection.Point) 
                ? AddInBounds(a, b, intersection.Point)
                : AddOutOfBounds(a, b, intersection.Point)
            select intersection;
        intersections.Consume();
        return hsi;

        static Unit AddOutOfBounds(HailStoneIntersection a, HailStoneIntersection b, PointD point)
        {
            a.AddOutsideBoundaryCollision(b, point);
            b.AddOutsideBoundaryCollision(a, point);
            return unit;
        }

        static Unit AddInBounds(HailStoneIntersection a, HailStoneIntersection b, PointD point)
        {
            a.AddCollision(b, point);
            b.AddCollision(a, point);
            return unit;
        }
    }
    // next: determine which hailstones intersect, but assume that the 
    // ones that intersect blow up and don't continue
}