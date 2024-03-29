namespace AdventOfCode2023.Domain;

public readonly record struct Boundary(NumberRange XAxis, NumberRange YAxis)
{
    public bool Contains(Point p) => XAxis.Contains(p.X) && YAxis.Contains(p.Y);

    public bool Contains(PointD p) => XAxis.Contains(p.X) && YAxis.Contains(p.Y);
}