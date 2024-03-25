namespace AdventOfCode2023.Domain;

public record struct Line(Point Start, Point End)
{
    public bool IsPoint() => Start == End;
    public bool Contains(Point currPoint)
    {
        if (this.IsPoint())
        {
            return Start == currPoint;
        }
        
        var (point1, point2) = (Start, End);
        var dxc = currPoint.X - point1.X;
        var dyc = currPoint.Y - point1.Y;

        var dxl = point2.X - point1.X;
        var dyl = point2.Y - point1.Y;

        var cross = dxc * dyl - dyc * dxl;

        if (cross != 0) return false;

        bool res;
        if (Math.Abs(dxl) >= Math.Abs(dyl))
            res = dxl > 0 ? 
                point1.X <= currPoint.X && currPoint.X <= point2.X :
                point2.X <= currPoint.X && currPoint.X <= point1.X;
        else
            res = dyl > 0 ? 
                point1.Y <= currPoint.Y && currPoint.Y <= point2.Y :
                point2.Y <= currPoint.Y && currPoint.Y <= point1.Y;

        return res;
    }

    public static implicit operator Line(PointRange pr) => new(pr.Start, pr.End);
}