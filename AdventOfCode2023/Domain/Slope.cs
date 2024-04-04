namespace AdventOfCode2023.Domain;

public readonly record struct Slope
{
    private readonly double _value;

    public Slope(double value) => _value = value;

    public bool IsNaN => Double.IsNaN(_value);

    public static Slope Find(Point start, Point end)
    {
        var slope = ((double)end.Y - start.Y) / ((double)end.X - start.X);
        return new Slope(slope);
    }
    
    public static double operator *(long i, Slope s) => i* s._value;
    public static double operator *(Slope s, long i) => i* s._value;
    public static double operator *(Slope s, double i) => i* s._value;

    public static double operator +(long i, Slope s) => (double)i + s._value;
    public static double operator +(double d, Slope s) => d + s._value;
    public static double operator +(Slope s, double d) => d + s._value;
    public static Slope operator -(Slope a, Slope b) => new(a._value - b._value);
    public static double operator /(double d, Slope s) => d / s._value;
}