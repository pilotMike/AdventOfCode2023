namespace AdventOfCode2023.Domain;

public sealed class ChallengePart
{
    private readonly string _part;
    public static ChallengePart Part1 { get; } = new ChallengePart("1");
    public static ChallengePart Part2 { get; } = new ChallengePart("2");

    private ChallengePart(string part) => _part = part;

    public override string ToString() => _part;
}