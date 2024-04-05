using AdventOfCode2023.Domain;
using AdventOfCode2023.Domain.Geometries;
using FluentAssertions;

namespace AdventOfCode2023Tests.Domain;

public class BoundaryTests
{
    [Theory]
    [InlineData(10, 11, true)]
    [InlineData(10, 20, true)]
    [InlineData(10, 21, false)]
    [InlineData(9, 21, false)]
    [InlineData(9, 20, false)]
    public void Contains_ForInclusiveBoundary_ReturnsCorrectResult(int x, int y, bool expected)
    {
        NumberRange range = new (new(10, true), new(20, true));
        var boundary = new Boundary(range, range);

        boundary.Contains(new PointD(x, y)).Should().Be(expected);
        boundary.Contains(new Point(x, y)).Should().Be(expected);
    }
    
    [Theory]
    [InlineData(10, 11, false)]
    [InlineData(10, 20, false)]
    [InlineData(10, 21, false)]
    [InlineData(9, 21, false)]
    [InlineData(9, 20, false)]
    [InlineData(11, 20, false)]
    [InlineData(11, 19, true)]
    public void Contains_ForExclusiveBoundary_ReturnsCorrectResult(int x, int y, bool expected)
    {
        NumberRange range = new (new(10, false), new(20, false));
        var boundary = new Boundary(range, range);

        boundary.Contains(new PointD(x, y)).Should().Be(expected);
        boundary.Contains(new Point(x, y)).Should().Be(expected);
    }
    
    [Theory]
    [InlineData(10, 11, true)]
    [InlineData(10.0000000000001, 11, true)]
    [InlineData(10, 20, true)]
    [InlineData(10, 21, false)]
    [InlineData(9, 21, false)]
    [InlineData(9, 20, false)]
    [InlineData(10.1, 19.9, true)]
    [InlineData(9.99999999999999, 20.1, false)]
    public void Contains_WithDoubles_ForInclusiveBoundary_ReturnsCorrectResult(double x, double y, bool expected)
    {
        NumberRange range = new (new(10, true), new(20, true));
        var boundary = new Boundary(range, range);

        boundary.Contains(new PointD(x, y)).Should().Be(expected);
    }
}