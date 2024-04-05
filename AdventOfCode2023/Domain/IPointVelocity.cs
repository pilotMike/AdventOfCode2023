using AdventOfCode2023.Domain.Geometries;

namespace AdventOfCode2023.Domain;

public interface IPointVelocity2
{
    Point Position { get; }
    Velocity Velocity { get; }
}

public interface IPointVelocity3
{
    Point3 Position { get; }
    Velocity3 Velocity { get; }
}