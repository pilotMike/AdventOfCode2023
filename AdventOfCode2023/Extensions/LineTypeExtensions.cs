using AdventOfCode2023.Domain;

namespace AdventOfCode2023.Extensions;

public static class LineTypeExtensions
{
    public static Ray ToRay<T>(this T pair) where T : IPointVelocity2 => new Ray(pair.Position, pair.Velocity);
}