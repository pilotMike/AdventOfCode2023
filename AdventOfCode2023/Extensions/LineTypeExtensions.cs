using AdventOfCode2023.Domain;
using AdventOfCode2023.Domain.Geometries;

namespace AdventOfCode2023.Extensions;

public static class LineTypeExtensions
{
    public static Ray ToRay<T>(this T pair) where T : IPointVelocity2 => new Ray(pair.Position, pair.Velocity);
    
    public static Ray3 ToRay3<T>(this T pair) where T : IPointVelocity3 => new Ray3(pair.Position, pair.Velocity);
}