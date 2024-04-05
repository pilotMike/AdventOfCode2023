using AdventOfCode2023.Domain.Geometries;

namespace AdventOfCode2023.Domain;

public record struct PointRange(Point Start, Point End)
{
    public long LengthX => Math.Abs(End.X - Start.X);
    
    /// <summary>Can have negative values or values beyond the array. Returns all points surrounding this item.
    /// Assumes Horizontal orientation
    /// </summary>
    public IEnumerable<Point> AdjacentPointsWithHorizontalOrientation()
    {
        var start = Start.X - 1;
        var length = LengthX + 2;
        var end = start + length;
        
        // return the row above with the extra width, row below, and two on the center
        // above
        var y = Start.Y - 1;
        for (long x = start; x <= end; x++)
            yield return new Point(x, y);
        
        // two in center
        yield return new Point(Start.X - 1, Start.Y);
        yield return new Point(End.X + 1, End.Y);
        
        // bottom row
        y = Start.Y + 1;
        for (long x = start; x <= end; x++)
            yield return new Point(x, y);
    }

    public bool Contains(Point point) => ((Line)this).Contains(point);
}