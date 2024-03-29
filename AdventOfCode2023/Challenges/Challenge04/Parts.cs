
using AdventOfCode2023.Domain;
using AdventOfCode2023.Extensions;

namespace AdventOfCode2023.Challenges.Challenge04;

public record struct HailStone(Point3 Position, Velocity3 Velocity)
{
    public HailStone2 ToHailStone2() => new HailStone2(Position.ToPoint2(), Velocity.ToVelocity2());
};

public record struct HailStone2(Point Position, Velocity Velocity) : IPointVelocity2;

public class HailStoneIntersection(HailStone2 hailStone, Option<(HailStoneIntersection Other, double Distance)> closestIntersection)
{
    public HailStone2 HailStone { get; } = hailStone;

    public Option<(HailStoneIntersection Other, double Distance)> ClosestIntersection { get; private set; } = closestIntersection;

    public Option<(HailStoneIntersection Other, double Distance)> SetClosestDistance(HailStoneIntersection other, PointD intersectionPoint)
    {
        var distance = other.HailStone.Position.ToPointD().Distance(intersectionPoint);
        ClosestIntersection =
            ClosestIntersection
                .Where(c => c.Distance > distance)
                .Bind(c => ClosestIntersection = (other, distance));
        return ClosestIntersection;
    }
}

public static class Parts
{
    public static int Part1(IChallenge04Input input, Boundary boundary) =>
        new Parser().Parse(input)
            .Map(x => new HailStoneIntersection(x.ToHailStone2(), default))
            .Apply(hs =>
            {
                // set the closest intersection points for each piece of hail
                var intersections =
                    from a in hs
                    from b in hs
                    where a != b
                    let rayA = a.HailStone.ToRay()
                    let rayB = b.HailStone.ToRay()
                    from intersection in IntersectionModule.Intersection(rayA, rayB)
                    where boundary.Contains(intersection)
                    let _ = a.SetClosestDistance(b, intersection)
                    let __ = b.SetClosestDistance(a, intersection)
                    select a;
                intersections.Consume();
                return hs;
            })
        
            .Apply(_ => 1);
    // next: determine which hailstones intersect, but assume that the 
    // ones that intersect blow up and don't continue
}