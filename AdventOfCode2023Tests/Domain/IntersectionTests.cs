using AdventOfCode2023.Challenges.Challenge04;
using AdventOfCode2023.Challenges.Challenge24;
using AdventOfCode2023.Domain;
using AdventOfCode2023.Domain.Geometries;
using AdventOfCode2023Tests.Extensions;
using FluentAssertions;

namespace AdventOfCode2023Tests.Domain;

public class IntersectionTests
{
    [Theory]
    [InlineData(
        19,13,30,-2,1,-2,
        18, 19, 22, -1, -1, -2,
        14.333d, 15.333d)]
    [InlineData(
        19, 13, 30, -2, 1, -2,
        20, 25, 34, -2, -2, -4,
        11.667d, 16.667d)]
    [InlineData(
        19, 13, 30, -2, 1, -22,
        12, 31, 28, -1, -2, -1,
        6.2d, 19.4d)]
    [InlineData(
        18, 19, 22, -1, -1, -2,
        20, 25, 34, -2, -2, -4,
        null, null)]
    [InlineData(
        18, 19, 22, -1, 0, -2,
        20, 25, 34, -2, 0, -4,
        null, null)]
    [InlineData(
        18, 19, 22, 0, -1, -2,
        20, 25, 34, 0, -2, -4,
        null, null)]
    public static void Intersections(
        int x, int y, int z, int vx, int vy, int vz,
        int x2, int y2, int z2, int vx2, int vy2, int vz2,
        double? expectedX, double? expectedY)
    {
        var hailstoneA = new HailStone(new Point3(x, y, z), new Velocity3(vx, vy, vz));
        var hailstoneB = new HailStone(new Point3(x2, y2, z2), new Velocity3(vx2, vy2, vz2));

        var aLine = new Ray(hailstoneA.Position.ToPoint2(), hailstoneA.Velocity.ToVelocity2());
        var bLine = new Ray(hailstoneB.Position.ToPoint2(), hailstoneB.Velocity.ToVelocity2());

        var intersection = IntersectionModule.Intersection(aLine, bLine);

        var expected = 
            from xx in expectedX.ToOption() 
            from yy in expectedY.ToOption() 
            select new PointD(xx, yy);

        _ = expected.Match(
            e => intersection.ShouldBeSome(i => i.Should().Be(e)),
            () => intersection.ShouldBeNone());
    }
}