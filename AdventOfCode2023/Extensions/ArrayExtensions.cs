using AdventOfCode2023.Domain;

namespace AdventOfCode2023.Extensions;

public static class ArrayExtensions
{
    public static IEnumerable<TOut> SelectAll<T, TOut>(this T[,] array, Func<Point, T, TOut> projection)
    {
        var yLength = array.GetUpperBound(0);
        var xLength = array.GetUpperBound(1);

        _ = array[yLength, xLength];
        
        for(int y = 0; y <= yLength; y++)
        for (int x = 0; x <= xLength; x++)
        {
            yield return projection(new Point(x, y), array[y, x]);
        }
    }

    public static Option<T> Find<T>(this T[,] array, Point point)
    {
        if (point.X < 0 || point.Y < 0) return Option<T>.None;
        var yLength = array.GetUpperBound(0);
        var xLength = array.GetUpperBound(1);
        
        if (point.X > xLength) return Option<T>.None;
        if (point.Y > yLength) return Option<T>.None;

        return array[point.Y, point.X];
    }
}