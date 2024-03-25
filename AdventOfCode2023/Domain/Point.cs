namespace AdventOfCode2023.Domain;

public record struct Point(int X, int Y)
{
    public IEnumerable<Point> GetAdjacentPoints() =>
        new PointRange(this, this).AdjacentPointsWithHorizontalOrientation();
}