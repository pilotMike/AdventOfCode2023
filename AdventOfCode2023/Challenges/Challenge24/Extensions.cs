namespace AdventOfCode2023.Challenges.Challenge24;

public static class Extensions
{
    public static HailStone2 ToHailStone2(this HailStone hailStone) =>
        new HailStone2(hailStone.Position.ToPoint2(), hailStone.Velocity.ToVelocity2());
}