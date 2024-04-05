using AdventOfCode2023.Domain;
using AdventOfCode2023.Domain.Geometries;
using FluentAssertions;

namespace AdventOfCode2023Tests.Domain;

public class PointRangeExtensions
{
    [Fact]
    public void AdjacentPointsWithHorizontalOrientationTest()
    {
        var p = new PointRange(new Point(5, 5), new Point(10, 5));

        p.LengthX.Should().Be(5);

        var surrounding = p.AdjacentPointsWithHorizontalOrientation().ToArray();

        var top = Enumerable.Range(4, 8).Select(i => new Point(i, 4));
        var centers = Seq(new Point(4, 5), new Point(11, 5));
        var bottom = Enumerable.Range(4, 8).Select(i => new Point(i, 6));

        var allExpected = top.Append(centers).Append(bottom);

        surrounding.Should().BeEquivalentTo(allExpected);
    }
}