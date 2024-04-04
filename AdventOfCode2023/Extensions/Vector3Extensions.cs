using System.Numerics;

namespace AdventOfCode2023.Extensions;

public static class Vector3Extensions
{
    public static float SqrMagnitude(this Vector3 vector) => Vector3.Dot(vector, vector);
}