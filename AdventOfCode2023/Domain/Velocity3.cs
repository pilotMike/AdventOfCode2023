namespace AdventOfCode2023.Domain;

public readonly record struct Velocity(int X, int Y)
{
    public static double operator *(int i, Velocity s) => i * s.ToSlope2();

    public Slope ToSlope2() => X == 0 ? new Slope(Double.NaN) : new Slope((double)Y / X);
}

public readonly record struct Velocity3(int X, int Y, int Z)
{
    public static explicit operator Velocity(Velocity3 v3) => new(v3.X, v3.Y);

    public Slope ToSlope2() => new Slope((double)Y / (double)X);
    public Velocity ToVelocity2() => new Velocity(X, Y);
    public static explicit operator Velocity3((int x, int y, int z) t) => new (t.x, t.y, t.z);

}

public readonly record struct Vector(int X, int Y);

public readonly record struct Vector3(int X, int Y, int Z)
{
    public static explicit operator Vector(Vector3 v3) => new(v3.X, v3.Y);
}